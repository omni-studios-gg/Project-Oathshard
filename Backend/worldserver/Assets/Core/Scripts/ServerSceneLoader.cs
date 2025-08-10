using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerSceneLoader : MonoBehaviour
{
    public string sceneToLoad = "main_world";  // change to your scene name

    void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            // Load and sync the scene after server starts
            NetworkManager.Singleton.SceneManager.LoadScene(sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Single);
            Debug.Log($"Server is loading scene: {sceneToLoad}");
        }
    }
}