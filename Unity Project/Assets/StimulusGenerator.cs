using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Diagnostics;

public class StimulusGenerator : MonoBehaviour
{
    public GameObject stimulusPrefab;
    public Transform semisphereTransform;
    public string filePath; // File path to the configuration file

    private int numberOfStimuli;
    private float dBStartValue;
    private float responseTime;
    private List<Vector3> stimulusPositions = new List<Vector3>();
    private List<StimulusInfo> stimulusInfoList = new List<StimulusInfo>();
    private int stimuliDisplayed = 0;
    private bool isGenerating = false;
    private StreamWriter resultsWriter;
    private float stimulusGenerationTime;
    private bool triggerPressed = false; // Indicates if the trigger is pressed

    // Start is called before the first frame update
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

        RecordResults();
    }

    private GameObject CreateStimulus(Vector3 localPosition)
    {
        Vector3 worldPosition = semisphereTransform.TransformPoint(localPosition);
        GameObject stimulus = Instantiate(stimulusPrefab, worldPosition, Quaternion.identity);
        stimulus.transform.SetParent(semisphereTransform);
        stimulusGenerationTime = Time.time;

        Renderer renderer = stimulus.GetComponent<Renderer>();
        renderer.material.color = dBToColor(dBStartValue - stimuliDisplayed);

        stimulusInfoList.Add(new StimulusInfo(stimuliDisplayed, false, localPosition));

        return stimulus;
    }



    private Color dBToColor(float dBValue)
    {
        float linearValue = Mathf.Pow(10, dBValue / 10.0f);
        float alpha = linearValue / 100.0f; // Calculate alpha based on dBValue

        // Clamp the alpha value to the range [0, 1]
        alpha = Mathf.Clamp(alpha, 0.0f, 1.0f);

        UnityEngine.Debug.Log($"dBValue: {dBValue}, Alpha: {alpha}");

        // Use alpha for the transparency of the color
        return new Color(1, 1, 1, 0); // RGB values are set to 1 to represent pure light
    }



    public void OnTriggerPulled()
    {
        triggerPressed = true;
        if (isGenerating)
        {
            float currentTime = Time.time;
            float timePassed = currentTime - stimulusGenerationTime;

            // Check if the trigger was pulled within response time
            if (timePassed <= responseTime)
            {
                RecordResponse(true); // Record as positive response
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

    // Class to store stimulus information
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
