using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildPostProcessor : IPostprocessBuildWithReport
{
    // Set execution order priority (optional)
    public int callbackOrder => 0;

    // Default server config JSON content
    private const string defaultConfigJson = 
        @"{
  ""ipAddress"": ""127.0.0.1"",
  ""port"": 7777
}";

    public void OnPostprocessBuild(BuildReport report)
    {
        string buildPath = report.summary.outputPath;

        // For Windows standalone build, outputPath is usually: YourFolder/YourGame.exe
        string buildFolder = Path.GetDirectoryName(buildPath);

        if (string.IsNullOrEmpty(buildFolder))
        {
            Debug.LogError("[BuildPostProcessor] Could not determine build folder path.");
            return;
        }

        // Create ServerConfig folder inside build folder
        string serverConfigFolder = Path.Combine(buildFolder, "ServerConfig");

        if (!Directory.Exists(serverConfigFolder))
        {
            Directory.CreateDirectory(serverConfigFolder);
            Debug.Log("[BuildPostProcessor] Created ServerConfig folder at: " + serverConfigFolder);
        }

        // Write default serverconfig.json file if it doesn't exist yet
        string configFilePath = Path.Combine(serverConfigFolder, "serverconfig.json");

        if (!File.Exists(configFilePath))
        {
            File.WriteAllText(configFilePath, defaultConfigJson);
            Debug.Log("[BuildPostProcessor] Created default serverconfig.json");
        }
        else
        {
            Debug.Log("[BuildPostProcessor] serverconfig.json already exists, skipping creation.");
        }
    }
}