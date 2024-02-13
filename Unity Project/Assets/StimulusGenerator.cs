using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Diagnostics;

public class StimulusGenerator : MonoBehaviour
{
    public GameObject stimulusPrefab; // prefab of stimulus to be generated
    public Transform Canvas; // where stimulus are generated on.
    public string filePath; // File path to the test file
    public EyeTracking eyeTracking; // reference to the eyetracking script

    private int numberOfStimuli;
    private float dBStartValue;
    private float responseTime;
    private List<Vector3> stimulusPositions = new List<Vector3>();
    private List<StimulusInfo> stimulusInfoList = new List<StimulusInfo>();
    private int stimuliDisplayed = 0;
    private bool isGenerating = false;
    private StreamWriter resultsWriter;
    private float stimulusGenerationTime;
    private bool triggerPressed = false;


    void Start()
    {
        ReadStimuliFile();
        resultsWriter = new StreamWriter("stimulus_results.txt", true);
        StartCoroutine(GenerateStimuli());
    }

    private void ReadStimuliFile()
    {
        string[] lines = File.ReadAllLines(filePath);
        numberOfStimuli = int.Parse(lines[0]);
        dBStartValue = float.Parse(lines[1]);
        responseTime = float.Parse(lines[2]);

        for (int i = 3; i < lines.Length; i++)
        {
            string line = lines[i].Trim('<', '>');
            string[] coords = line.Split(',');
            Vector3 position = new Vector3(
                float.Parse(coords[0]),
                float.Parse(coords[1]),
                float.Parse(coords[2])
            );
            stimulusPositions.Add(position);
        }
    }

    private IEnumerator GenerateStimuli()
    {
        GameObject currentStimulus = null;

        while (stimuliDisplayed < numberOfStimuli)
        {
            if (!isGenerating)
            {
                isGenerating = true;

                float randomDelay = UnityEngine.Random.Range(1.0f, 5.0f);
                yield return new WaitForSeconds(randomDelay);

                currentStimulus = CreateStimulus(stimulusPositions[stimuliDisplayed]);
                stimuliDisplayed++;
                yield return new WaitForSeconds(responseTime);

                if (currentStimulus != null)
                {
                    Destroy(currentStimulus);
                }

                isGenerating = false;
            }
            else
            {
                yield return null;
            }
        }
        if(eyeTracking != null)
        {
            eyeTracking.StopTracking();
        }
        else
        {
            UnityEngine.Debug.Log("Problem stopping eyeTracking.StopTracking() in StimulusGenerator");
        }
        RecordResults();
    }

    private GameObject CreateStimulus(Vector3 localPosition)
    {
        Vector3 worldPosition = Canvas.TransformPoint(localPosition);
        GameObject stimulus = Instantiate(stimulusPrefab, worldPosition, Quaternion.identity);
        stimulus.transform.SetParent(Canvas);
        stimulusGenerationTime = Time.time;

        Renderer renderer = stimulus.GetComponent<Renderer>();
        renderer.material.color = dBToColor(dBStartValue - stimuliDisplayed);

        stimulusInfoList.Add(new StimulusInfo(stimuliDisplayed, false, localPosition));

        return stimulus;
    }



    private Color dBToColor(float dBValue)
    {
        float linearValue = Mathf.Pow(10, dBValue / 10.0f);
        float alpha = linearValue / 100.0f;


        alpha = Mathf.Clamp(alpha, 0.0f, 1.0f);

        UnityEngine.Debug.Log($"dBValue: {dBValue}, Alpha: {alpha}");

        return new Color(1, 1, 1, 0);
    }



    public void OnTriggerPulled()
    {
        triggerPressed = true;
        if (isGenerating)
        {
            float currentTime = Time.time;
            float timePassed = currentTime - stimulusGenerationTime;


            if (timePassed <= responseTime)
            {
                RecordResponse(true);
            }
        }
    }

    public void OnTriggerReleased()
    {
        triggerPressed = false;
    }

    private void RecordResponse(bool success)
    {
        stimulusInfoList[stimuliDisplayed - 1].Response = success;
    }

    private void RecordResults()
    {
        foreach (StimulusInfo stimulusInfo in stimulusInfoList)
        {
            string result = $"{stimulusInfo.Index} {(stimulusInfo.Response ? 'Y' : 'N')} {stimulusInfo.Coordinates}";
            resultsWriter.WriteLine(result);
        }

        resultsWriter.Close();
    }


    public class StimulusInfo
    {
        public int Index { get; set; }
        public bool Response { get; set; }
        public Vector3 Coordinates { get; set; }

        public StimulusInfo(int index, bool response, Vector3 coordinates)
        {
            Index = index;
            Response = response;
            Coordinates = coordinates;
        }
    }
}
