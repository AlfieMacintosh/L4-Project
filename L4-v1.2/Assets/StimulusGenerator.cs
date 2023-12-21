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

    private void CreateStimulus()
    {

        float radius = 1.5f;
        float angle = UnityEngine.Random.Range(0f, 360f);
        Vector3 position = semisphereTransform.position + Quaternion.Euler(0, angle, 0) * (Vector3.forward * radius);


        GameObject stimulus = Instantiate(stimulusPrefab, position, Quaternion.identity);


        stimulus.transform.SetParent(semisphereTransform);


        Renderer renderer = stimulus.GetComponent<Renderer>();
        renderer.material.color = Color.red;
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
