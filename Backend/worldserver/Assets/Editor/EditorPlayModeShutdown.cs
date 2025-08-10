#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Unity.Netcode;

[InitializeOnLoad]
public static class EditorPlayModeShutdown
{
    static EditorPlayModeShutdown()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            if (NetworkManager.Singleton != null)
            {
                Debug.Log("Editor exiting play mode: shutting down NetworkManager.");
                NetworkManager.Singleton.Shutdown();
            }
        }
    }
}
#endif