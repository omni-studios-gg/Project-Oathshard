using System.Collections;
using System.Collections.Generic;
using Omni;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private HelloWorld.GameState currentState;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = HelloWorld.GameState.Starting;
        Debug.Log("CurrentState:" + currentState);
    }
    
    

   
}
