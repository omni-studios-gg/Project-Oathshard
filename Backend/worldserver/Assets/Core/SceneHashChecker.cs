using UnityEngine;
using Unity.Netcode;
using System.Text;

public class SceneHashChecker : MonoBehaviour
{
    void Start()
    {
        string sceneName = "main_world.unity";
        uint hash = ComputeSceneHash(sceneName);
        Debug.Log($"Scene: {sceneName}  Hash: {hash}");
    }

    public static uint ComputeSceneHash(string sceneName)
    {
        // Unity Netcode uses Fowler–Noll–Vo hash function (FNV-1a) on the UTF8 bytes of the scene path
        var bytes = Encoding.UTF8.GetBytes(sceneName.ToLowerInvariant());
        const uint fnvPrime = 16777619;
        uint hash = 2166136261;

        for (int i = 0; i < bytes.Length; i++)
        {
            hash ^= bytes[i];
            hash *= fnvPrime;
        }

        return hash;
    }
}
