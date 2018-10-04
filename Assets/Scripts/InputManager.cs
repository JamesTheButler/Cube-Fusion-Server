using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public bool useDirectMovement;
    public bool useSequentialMovement;
    public bool useNetworkMovement;

    private PlayerMovement playerMovement;
//    private DirectInput directInput;
//    private SequentialInput seuqntialInput;
//    private NetworkInput networkInput;
    private SequentialMovement sequentialMovement;

    void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        sequentialMovement = GetComponent<SequentialMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (useDirectMovement) {
            Debug.Log("useDirectInput");
            Debug.Log("usePlayerMovement");

        } else if (useNetworkMovement) {
            Debug.Log("useSequentialInput");
            Debug.Log("useSequentialMovement");

        } else if (useSequentialMovement) {
            Debug.Log("useNetworkInput");
            Debug.Log("useSequentialMovement");

        }

    }
}
