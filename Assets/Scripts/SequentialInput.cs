using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialInput : MonoBehaviour {
    PlayerMovement playerMovement;
    bool haveToReadInputs = true;

    List<eCommands> playerOneMovement = new List<eCommands>();
    List<eCommands> playerTwoMovement = new List<eCommands>();


    void Start() {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    //Simulates the creation of a sequence (by pressing buttons wasd)
    //Then when the user presses return : reads the inputs in the console
    public IEnumerator readInput() {
        haveToReadInputs = true;
        yield return null;
        playerOneMovement.Clear();
        playerTwoMovement.Clear();

        Debug.Log("SequentialInput :: Waiting for Input");
        while (!Input.GetKeyDown(KeyCode.Return) && haveToReadInputs)
        {
            // Player One input            
            if (Input.GetKeyDown(KeyCode.D)) {
                registerInput(ePlayers.ONE, eCommands.RIGHT);
            } 
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                registerInput(ePlayers.ONE, eCommands.NONE);
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                registerInput(ePlayers.ONE, eCommands.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.W)) {
                registerInput(ePlayers.ONE, eCommands.UP);
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                registerInput(ePlayers.ONE, eCommands.DOWN);
            }
            
            //Player two input
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                registerInput(ePlayers.TWO, eCommands.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                registerInput(ePlayers.TWO, eCommands.NONE);
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                registerInput(ePlayers.TWO, eCommands.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                registerInput(ePlayers.TWO, eCommands.UP);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                registerInput(ePlayers.TWO, eCommands.DOWN);
            }
            yield return null;
            
        }

        Debug.Log("SequentialInput :: Starting movements");
        if(haveToReadInputs)
        playerMovement.moveTwoPlayers(playerOneMovement, playerTwoMovement);
    }

    public void stopWaitingForInputs()
    {
        haveToReadInputs = false;
        playerOneMovement.Clear();
        playerTwoMovement.Clear();   
        StopAllCoroutines();
    }

    private void registerInput(ePlayers player, eCommands command) {
        List<eCommands> playerList = player == ePlayers.ONE ? playerOneMovement : playerTwoMovement;
        playerList.Add(command);
        Debug.Log("SequentialInput :: Waiting for a new Input");
    }
}
