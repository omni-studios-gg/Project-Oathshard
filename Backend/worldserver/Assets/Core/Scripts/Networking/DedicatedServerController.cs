using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class DedicatedServerController : MonoBehaviour
{
    private bool isShuttingDown = false;
    private int port = 7777;
    private string ipAddress = "127.0.0.1";

    void Start()
    {
        LogInfo("Server initializing...");
        
        var config = ServerConfigLoader.LoadConfig();
        if (config == null)
        {
            LogError("Failed to load server config. Shutting down.");
            Application.Quit(1);
            return;
        }

        ipAddress = config.ipAddress;
        port = config.port;

        InitializeNetworkManager();
        StartServer();

        SubscribeToNetworkEvents();
    }

    private void InitializeNetworkManager()
    {
        var nm = NetworkManager.Singleton;
        if (nm == null)
        {
            LogError("NetworkManager.Singleton is null. Server cannot start.");
            Application.Quit(1);
            return;
        }

        var transport = nm.GetComponent<UnityTransport>();
        if (transport == null)
        {
            LogError("UnityTransport component missing on NetworkManager.");
            Application.Quit(1);
            return;
        }

        transport.SetConnectionData(ipAddress, (ushort)port);
        LogInfo($"Transport configured to listen on {ipAddress}:{port}");
    }

    private void StartServer()
    {
        var nm = NetworkManager.Singleton;
        if (!nm.StartServer())
        {
            LogError("Failed to start server.");
            Application.Quit(1);
            return;
        }
        LogInfo("Server started successfully and ready to accept clients.");
    }

    private void SubscribeToNetworkEvents()
    {
        var nm = NetworkManager.Singleton;
        if (nm == null) return;

        nm.OnClientConnectedCallback += HandleClientConnected;
        nm.OnClientDisconnectCallback += HandleClientDisconnected;
    }

    private void UnsubscribeFromNetworkEvents()
    {
        var nm = NetworkManager.Singleton;
        if (nm == null) return;

        nm.OnClientConnectedCallback -= HandleClientConnected;
        nm.OnClientDisconnectCallback -= HandleClientDisconnected;
    }

    private void HandleClientConnected(ulong clientId)
    {
        LogInfo($"Client connected! ClientId: {clientId}, Timestamp: {DateTime.Now:HH:mm:ss}");
        // Optionally add more info about client here if you have it (like IP or username)
    }

    private void HandleClientDisconnected(ulong clientId)
    {
        LogWarning($"Client disconnected! ClientId: {clientId}, Timestamp: {DateTime.Now:HH:mm:ss}");
    }

    public void ShutdownServer()
    {
        if (isShuttingDown) return;

        isShuttingDown = true;
        UnsubscribeFromNetworkEvents();

        var nm = NetworkManager.Singleton;
        if (nm == null)
        {
            LogWarning("NetworkManager.Singleton null during shutdown.");
            return;
        }

        if (nm.IsServer)
        {
            LogInfo("Initiating graceful shutdown...");
            nm.Shutdown();
            LogInfo("Shutdown complete.");
        }
    }

#if !UNITY_EDITOR
    void OnApplicationQuit()
    {
        ShutdownServer();
    }

    void OnDestroy()
    {
        ShutdownServer();
    }
#endif

    // --- Helper logging methods for consistent, color-coded output ---

    private void LogInfo(string message)
    {
        Debug.Log($"<color=green>[DedicatedServer INFO]</color> {message}");
    }

    private void LogWarning(string message)
    {
        Debug.LogWarning($"<color=orange>[DedicatedServer WARN]</color> {message}");
    }

    private void LogError(string message)
    {
        Debug.LogError($"<color=red>[DedicatedServer ERROR]</color> {message}");
    }
}
