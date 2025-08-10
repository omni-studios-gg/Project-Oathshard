using UnityEngine;
using Omni;

public class HelloWorldTest : MonoBehaviour
{
    private HelloWorld helloWorld;

    void Start()
    {
        // Add the DLL MonoBehaviour as a component
        helloWorld = gameObject.AddComponent<HelloWorld>();
        helloWorld.SayHello();
        
    }
}