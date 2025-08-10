using System.IO;
using UnityEngine;

public static class ServerConfigLoader
{
    public static string GetConfigFolder()
    {
        // Application.dataPath ends with ".../YourGame_Data"
        // We want the folder one level up, where the exe lives
        var exeFolder = Directory.GetParent(Application.dataPath).FullName;
        return Path.Combine(exeFolder, "ServerConfig");
    }

    public static string GetConfigPath(string fileName)
    {
        return Path.Combine(GetConfigFolder(), fileName);
    }

    public static ServerConfig LoadConfig(string fileName = "serverconfig.json")
    {
        var folder = GetConfigFolder();

        if (!Directory.Exists(folder))
        {
            Debug.LogWarning($"[ServerConfigLoader] Config folder not found: {folder}. Creating...");
            Directory.CreateDirectory(folder);
        }

        var path = GetConfigPath(fileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"[ServerConfigLoader] Config file not found at {path}");
            return null;
        }

        try
        {
            var json = File.ReadAllText(path);
            var config = JsonUtility.FromJson<ServerConfig>(json);
            Debug.Log($"[ServerConfigLoader] Loaded config from {path}");
            return config;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[ServerConfigLoader] Failed to load config: {e.Message}");
            return null;
        }
    }
}