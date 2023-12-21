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

    public float stimulusInterval = 3.0f;
    public int numberOfStimuli = 20;

    private int stimuliDisplayed = 0;
    private bool isGenerating = false;
    private List<bool> stimulusResponses = new List<bool>();

    private StreamWriter resultsWriter;

    private void Start()
    {
        resultsWriter = new StreamWriter("stimulus_results.txt");
        StartCoroutine(GenerateStimuli());
    }


    private IEnumerator GenerateStimuli()
    {
        while (stimuliDisplayed < numberOfStimuli)
        {
            if (!isGenerating)
            {
                isGenerating = true;


                CreateStimulus();

                stimuliDisplayed++;


                yield return new WaitForSeconds(stimulusInterval);

                isGenerating = false;
            }
            else
            {

                yield return null;
            }
        }


        RecordResults();
    }

    private int[] ringStimuliCounts = { 20, 16, 14, 12, 4 };
    private float[] ringRadii = { 0.98f, 0.92f, 0.8f, 0.48f, 0.15f };
    private float[] ringYCoordinates = { 0f, 0.38f, 0.6f, 0.85f, 0.95f };

    private int currentRing = 0;
    private int currentStimuliCount = 0;

    private void CreateStimulus()
    {

        float radius = ringRadii[currentRing];


        float yCoordinate = ringYCoordinates[currentRing];


        float angle = (360f / ringStimuliCounts[currentRing]) * currentStimuliCount;


        Vector3 localPosition = Quaternion.Euler(0, angle, 0) * (Vector3.forward * radius);


        localPosition.y = yCoordinate;


        Vector3 position = semisphereTransform.TransformPoint(localPosition);



        GameObject stimulus = Instantiate(stimulusPrefab, position, Quaternion.identity);

        stimulus.transform.SetParent(semisphereTransform);

        Renderer renderer = stimulus.GetComponent<Renderer>();
        renderer.material.color = Color.red;


        currentStimuliCount++;

        if (currentStimuliCount >= ringStimuliCounts[currentRing])
        {
            currentRing++;
            currentStimuliCount = 0;
        }
    }





    public void RecordResponse(bool success)
    {

        stimulusResponses.Add(success);
    }

    private void RecordResults()
    {

        for (int i = 0; i < stimulusResponses.Count; i++)
        {
            string result = $"{i} [{(stimulusResponses[i] ? 'Y' : 'N')}]";
            resultsWriter.WriteLine(result);
        }


        resultsWriter.Close();
    }
}
