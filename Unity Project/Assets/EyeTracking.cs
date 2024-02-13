using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using ViveSR.anipal.Eye;

public class EyeTracking : MonoBehaviour
{
    public GameObject StimulusObject; // Assign the stimulus object in the inspector
    private LineRenderer gazeRayRenderer;
    private readonly float MaxDistance = 20;
    private static EyeData eyeData = new EyeData();
    private bool eye_callback_registered = false;
    private StreamWriter outputStream;
    private string outputPath = "EyeTrackingData.txt";
    private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };

    void Start()
    {
        gazeRayRenderer = GetComponent<LineRenderer>();
        outputStream = new StreamWriter(outputPath, true);
    }

    private void Update()
    {
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
            SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

        if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && !eye_callback_registered)
        {
            SRanipal_Eye.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
            eye_callback_registered = true;
        }
        else if (!SRanipal_Eye_Framework.Instance.EnableEyeDataCallback && eye_callback_registered)
        {
            SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }

        Ray gazeRay;
        bool eye_focus = false;
        FocusInfo focusInfo = new FocusInfo();
        int stimulusLayerId = LayerMask.NameToLayer("stimulusToTrack");
        LayerMask layerMask = 1 << stimulusLayerId;

        // Always check the combined gaze.
        GazeIndex gazeIndex = GazeIndex.COMBINE;

        if (eye_callback_registered)
        {
            // Use the eye tracking data from the callback
            eye_focus = SRanipal_Eye.Focus(gazeIndex, out gazeRay, out focusInfo, 0, MaxDistance, layerMask, eyeData);
        }
        else
        {
            // Use the standard method to get eye tracking data
            eye_focus = SRanipal_Eye.Focus(gazeIndex, out gazeRay, out focusInfo, 0, MaxDistance, layerMask);
        }

        if (StimulusObject != null && focusInfo.transform != null && eye_focus)
        {
            HighlightObject(eye_focus && focusInfo.transform.gameObject == StimulusObject);
        }
        else
        {
            HighlightObject(false); // If not focused or the StimulusObject is null
        }

        if (eye_focus)
        {
            // Log hit point
            outputStream.WriteLine($"{Time.time},{focusInfo.point.x},{focusInfo.point.y},{focusInfo.point.z}");
            UnityEngine.Debug.Log("Focus on:" + focusInfo.transform?.name);
        }
    }

    void HighlightObject(bool highlight)
    {
        centralStimulus centralStimulus = StimulusObject.GetComponent<centralStimulus>();
        if (centralStimulus != null)
        {
            centralStimulus.Highlight(highlight);
        }
    }

    private void OnApplicationQuit()
    {
        if (outputStream != null)
        {
            outputStream.Close();
        }
    }

    private static void EyeCallback(ref EyeData eye_data)
    {
        eyeData = eye_data;
    }
}
