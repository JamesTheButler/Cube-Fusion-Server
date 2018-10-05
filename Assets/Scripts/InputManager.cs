using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public bool useDirectMovement;
    public bool useSequentialMovement;
    public bool useNetworkMovement;

    private PlayerMovement playerMovement;
    private DirectInput directInput;
        
    void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        directInput = GetComponent<DirectInput>();
    }
	
	void Update () {
        // handle real time input
        if (useDirectMovement && !playerMovement.isPerformingAction) {
            if(directInput.readInput(ePlayers.ONE) != eCommands.NONE)
                StartCoroutine(playerMovement.playerAction(ePlayers.ONE, directInput.readInput(ePlayers.ONE)));
            if (directInput.readInput(ePlayers.TWO) != eCommands.NONE)
                StartCoroutine(playerMovement.playerAction(ePlayers.TWO, directInput.readInput(ePlayers.TWO)));
       }
    }

    public void reinit() {
        reinitSequentialInput();
        reinitNetworkInput();
       // reinitPlayerMovement();
    }

    // reset 
    /*private void reinitPlayerMovement() {
        playerMovement.stopRunningActions();
    }*/

    // restart the sequential input functionality
    public void reinitSequentialInput() {
        if (useSequentialMovement) {
            Debug.Log("InputManager :: reinit sequential movement");
            SequentialInput sequentialMovement = FindObjectOfType<SequentialInput>();
            StopCoroutine(sequentialMovement.readInput());
            StartCoroutine(sequentialMovement.readInput());
        }
    }

    public void reinitNetworkInput() {
        if (useNetworkMovement) {
            Debug.LogError("not implemented yet");
        }
    }
}
