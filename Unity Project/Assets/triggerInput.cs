using UnityEngine;
using Valve.VR;

public class triggerInput : MonoBehaviour
{
    public StimulusGenerator stimulusGenerator;
    private SteamVR_Behaviour_Pose controllerPose;
    private SteamVR_Input_Sources inputSource;
    private SteamVR_Action_Boolean backTriggerAction;

    private bool triggerPressed = false;

    private void Awake()
    {
        controllerPose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        inputSource = controllerPose.inputSource;

        backTriggerAction = SteamVR_Actions.default_InteractUI;
    }

    private void Update()
    {
        if (backTriggerAction[inputSource].stateDown)
        {
            UnityEngine.Debug.Log("Trigger Down");
            if (!triggerPressed)
            {
                triggerPressed = true;

                if (stimulusGenerator != null)
                {
                    stimulusGenerator.OnTriggerPulled();
                }
            }
        }
        else if (backTriggerAction[inputSource].stateUp)
        {
            UnityEngine.Debug.Log("Trigger Up");
            triggerPressed = false;
        }
    }
}
