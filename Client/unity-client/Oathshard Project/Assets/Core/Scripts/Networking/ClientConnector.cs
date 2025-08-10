#if UNITY_EDITOR
using UnityEditor;
#endif

using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ClientConnector : MonoBehaviour
{
    [Tooltip("Server IP to connect to")]
    public string serverIP = "127.0.0.1";

    [Tooltip("Server port to connect to")]
    public ushort serverPort = 7777;

    void Start()
    {
        var nm = NetworkManager.Singleton;
        if (nm == null)
        {
            Debug.LogError("[Client] NetworkManager.Singleton is null. Cannot connect.");
            return;
        }

        var transport = nm.GetComponent<UnityTransport>();
        if (transport == null)
        {
            Debug.LogError("[Client] UnityTransport component missing on NetworkManager.");
            return;
        }

        transport.SetConnectionData(serverIP, serverPort);
        Debug.Log($"[Client] Connecting to server at {serverIP}:{serverPort}...");

        nm.OnClientConnectedCallback += OnClientConnected;
        nm.OnClientDisconnectCallback += OnClientDisconnected;

        nm.StartClient();
    }

    private void OnDestroy()
    {
        var nm = NetworkManager.Singleton;
        if (nm != null)
        {
            nm.OnClientConnectedCallback -= OnClientConnected;
            nm.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("[Client] Connected to server!");
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            Debug.LogWarning("[Client] Disconnected from server.");

#if UNITY_EDITOR
            // Stop play mode when testing inside the Editor
            EditorApplication.isPlaying = false;
#else
            // Quit the application when running a build
            Application.Quit();
#endif
        }
    }
}