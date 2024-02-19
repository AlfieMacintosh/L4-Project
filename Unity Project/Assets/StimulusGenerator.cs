using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StimulusGenerator : MonoBehaviour
{
    public GameObject stimulusPrefab; // Prefab for generating stimulus objects
    public Transform Canvas; // Plane for stimulus placement
    public string filePath; // Path to test, defined later in Start()
    public EyeTracking eyeTracking; // Reference to EyeTracking script

    private int numberOfStimuli; // Total number of stimuli to be generated, read in from file
    private float responseTime; // 2 second response time, can be altered, read in
    private List<Vector3> stimulusPositions = new List<Vector3>(); // Positions for stimuli generation, read in

    private List<StimulusInfo> stimulusInfoList = new List<StimulusInfo>(); // Info for each stimulus, time, coords, false positive. Check StimulusInfo class at bottom for all variables and get/set methods
    private List<FalsePositiveInfo> falsePositiveInfoList = new List<FalsePositiveInfo>(); // Similiar structure to stimusInfoList, used to track data on false positives, check bottom for class.
    private int stimuliDisplayed = 0; // Counter for the number of stimuli displayed
    private bool isGenerating = false; // Flag to control stimulus creation
    private StreamWriter resultsWriter; // For logging stimulus response results
    private StreamWriter falsePositivesWriter; // For logging false positives
    private float stimulusGenerationTime; // Timestamp for the generation of the current stimulus
    private float testStartTime; // Timestamp for the start of the test
    private bool waitingForNextStimulus = false; // Flag to indicate waiting period between stimuli
    private bool triggerPressed = false; // Flag to track the state of the trigger

    private string logFolderPath = "Data/Logs"; // New variable to be used to log data dynamically
    private string currentRunPath; // To be used in start to write files to new folder path


    void Start()
    {
        testStartTime = Time.time; // Record the start time of the test
        SetupFilePath();
        ReadStimuliFile();

        if (eyeTracking != null)
        {
            eyeTracking.SetOutputPath(currentRunPath);
        }

        resultsWriter = new StreamWriter(Path.Combine(currentRunPath, "stimulus_results.txt"), true);
        falsePositivesWriter = new StreamWriter(Path.Combine(currentRunPath, "false_positives.txt"), true);
        StartCoroutine(GenerateStimuli());
    }

    private void SetupFilePath()
    {
        if (!Directory.Exists(logFolderPath))
        {
            Directory.CreateDirectory(logFolderPath);
        }

        int runNumber = 1;
        while (Directory.Exists(Path.Combine(logFolderPath, $"run{runNumber}")))
        {
            runNumber++;
        }

        currentRunPath = Path.Combine(logFolderPath, $"run{runNumber}");
        Directory.CreateDirectory(currentRunPath);
    }

    private void ReadStimuliFile()
    {
        string[] lines = File.ReadAllLines(filePath);
        numberOfStimuli = int.Parse(lines[0]);
        responseTime = float.Parse(lines[1]);

        for (int i = 2; i < lines.Length; i++)
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
                waitingForNextStimulus = false;

                float randomDelay = UnityEngine.Random.Range(1.0f, 3.0f);
                yield return new WaitForSeconds(randomDelay);

                currentStimulus = CreateStimulus(stimulusPositions[stimuliDisplayed]);
                stimuliDisplayed++;
                yield return new WaitForSeconds(responseTime);

                if (currentStimulus != null)
                {
                    Destroy(currentStimulus);
                }

                isGenerating = false;
                waitingForNextStimulus = true;
                yield return new WaitForSeconds(randomDelay);
                waitingForNextStimulus = false;
            }
            else
            {
                yield return null;
            }
        }
        if (eyeTracking != null)
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

        stimulusInfoList.Add(new StimulusInfo(stimuliDisplayed, false, localPosition));

        return stimulus;
    }


    public void OnTriggerPulled()
    {
        triggerPressed = true;
        float currentTime = Time.time;

        
        if (isGenerating && stimuliDisplayed > 0)
        {
            float timePassed = currentTime - stimulusGenerationTime;
            bool wasLookingAtStimulus = eyeTracking.IsLookingAtStimulus();


            if (timePassed <= responseTime)
            {
                float responseTimeSinceTestStart = currentTime - testStartTime;
                RecordResponse(true, responseTimeSinceTestStart, timePassed, wasLookingAtStimulus);
            }
        }
        
        else if (waitingForNextStimulus && !isGenerating)
        {
            float timeSinceStart = currentTime - testStartTime;
            falsePositiveInfoList.Add(new FalsePositiveInfo(timeSinceStart));
        }
    }





    public void OnTriggerReleased()
    {
        triggerPressed = false;
    }

    private void RecordResponse(bool success, float responseTimeSinceTestStart, float timeSinceStimulusDisplayed, bool wasLookingAtStimulus)
    {
        if (stimuliDisplayed > 0)
        {
            var stimulusInfo = stimulusInfoList[stimuliDisplayed - 1];
            stimulusInfo.Response = success;
            stimulusInfo.ResponseTimeSinceTestStart = responseTimeSinceTestStart;
            stimulusInfo.TimeSinceStimulusDisplayed = timeSinceStimulusDisplayed;
            stimulusInfo.WasLookingAtStimulus = wasLookingAtStimulus;
        }
    }



    private void RecordResults()
    {
        foreach (StimulusInfo stimulusInfo in stimulusInfoList)
        {
            string result = $"{stimulusInfo.Index},{stimulusInfo.Coordinates},{stimulusInfo.Response},{stimulusInfo.ResponseTimeSinceTestStart},{stimulusInfo.TimeSinceStimulusDisplayed},{stimulusInfo.WasLookingAtStimulus}";

            resultsWriter.WriteLine(result);
        }
        resultsWriter.Close();

        foreach (FalsePositiveInfo fpInfo in falsePositiveInfoList)
        {
            string result = $"{fpInfo.TimeSinceStart}";
            falsePositivesWriter.WriteLine(result);
        }
        falsePositivesWriter.Close();

    }



    void OnDisable()
    {
        if (resultsWriter != null)
        {
            resultsWriter.Close();
        }
        if (falsePositivesWriter != null)
        {
            falsePositivesWriter.Close();
        }
    }

    void OnApplicationQuit()
    {
        OnDisable();
    }


    public class StimulusInfo
    {
        public int Index { get; set; }
        public bool Response { get; set; }
        public Vector3 Coordinates { get; set; }
        public bool FalsePositive { get; set; }
        public float ResponseTimeSinceTestStart { get; set; }
        public float TimeSinceStimulusDisplayed { get; set; }
        public bool WasLookingAtStimulus { get; set; }

        public StimulusInfo(int index, bool response, Vector3 coordinates)
        {
            Index = index;
            Response = response;
            Coordinates = coordinates;
        }
    }


    public class FalsePositiveInfo
    {
        public float TimeSinceStart { get; set; }

        public FalsePositiveInfo(float timeSinceStart)
        {
            TimeSinceStart = timeSinceStart;
        }
    }
}