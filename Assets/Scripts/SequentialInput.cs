using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialInput : MonoBehaviour {
    PlayerMovement playerMovement;

    List<eCommands> playerOneMovement = new List<eCommands>();
    List<eCommands> playerTwoMovement = new List<eCommands>();


    void Start() {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    //Simulates the creation of a sequence (by pressing buttons wasd)
    //Then when the user presses return : reads the inputs in the console
    public IEnumerator readInput() {
        yield return null;
        playerOneMovement.Clear();
        playerTwoMovement.Clear();

        Debug.Log("SequentialInput :: Waiting for Input");
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            // Player One input            
            if (Input.GetKeyDown(KeyCode.D)) {
                yield return StartCoroutine(registerInput(ePlayers.ONE, eCommands.RIGHT));
            } 
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                yield return StartCoroutine(registerInput(ePlayers.ONE, eCommands.NONE));
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                yield return StartCoroutine(registerInput(ePlayers.ONE, eCommands.LEFT));
            }
            else if (Input.GetKeyDown(KeyCode.W)) {
                yield return StartCoroutine(registerInput(ePlayers.ONE, eCommands.UP));
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                yield return StartCoroutine(registerInput(ePlayers.ONE, eCommands.DOWN));
            }
            
            //Player two input
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                yield return StartCoroutine(registerInput(ePlayers.TWO, eCommands.RIGHT));
            }
            else if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                yield return StartCoroutine(registerInput(ePlayers.TWO, eCommands.NONE));
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                yield return StartCoroutine(registerInput(ePlayers.TWO, eCommands.LEFT));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                yield return StartCoroutine(registerInput(ePlayers.TWO, eCommands.UP));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                yield return StartCoroutine(registerInput(ePlayers.TWO, eCommands.DOWN));
            }
            yield return null;
            
        }

        Debug.Log("SequentialInput :: Starting movements");
        playerMovement.moveTwoPlayers(playerOneMovement, playerTwoMovement);
    }

    public void stopWaitingForInputs()
    {
        playerOneMovement.Clear();
        playerTwoMovement.Clear();
        StopAllCoroutines();
    }

    private IEnumerator registerInput(ePlayers player, eCommands command) {
        List<eCommands> playerList = player == ePlayers.ONE ? playerOneMovement : playerTwoMovement;
        playerList.Add(command);
        yield return null;
        Debug.Log("SequentialInput :: Waiting for a new Input");
    }
}
