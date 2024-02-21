using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class ReportScripts : MonoBehaviour
{
    private string dataPath;
    private string scriptPath = "../scripts";

    public void SetDataPath(string path)
    {
        dataPath = path;
    }

    public void RunPythonScriptsSequence()
    {
        string pythonPath = @"C:\Program Files\Python38\python.exe";
        List<string> scriptsToRun = new List<string>
        {
            "heatmap.py",
            "grayscale.py",
            "invalid.py"

            // Add other scripts as needed, like "grayscale.py"
        };

        foreach (string scriptName in scriptsToRun)
        {
            RunPythonScript(pythonPath, scriptName);
        }
    }

    private void RunPythonScript(string pythonPath, string scriptName)
    {
        string scriptFullPath = Path.Combine(scriptPath, scriptName);
        string args = $"\"{scriptFullPath}\" \"{dataPath}\"";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = args,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true, // True redirects output to unity
            RedirectStandardError = true // True Redirects errors
        };

        using (Process process = Process.Start(psi))
        {
            // Read the output stream first and then wait.
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
            {
                UnityEngine.Debug.Log("Python Output: " + output);
            }

            if (!string.IsNullOrEmpty(error))
            {
                UnityEngine.Debug.LogError("Python Error: " + error);
            }
        }
    }
}
