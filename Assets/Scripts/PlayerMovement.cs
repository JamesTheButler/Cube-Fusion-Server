using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 3.0f;
    public float movementDelay = 0.1f;
    [HideInInspector]
    public bool isPerformingAction = false;
    public bool[] playerOneAvailableMovements = new bool[4];//Down - Up - Right - Left
    public bool[] playerTwoAvailableMovements = new bool[4];//Down - Up - Right - Left

    private void Start()
    {
        reInitAvailableMovements(ePlayers.ONE);
        reInitAvailableMovements(ePlayers.TWO);
    }

    public void moveTwoPlayers(List<eCommands> commandSequenceP1, List<eCommands> commandSequenceP2) {
        Debug.Log("SequentialMovement :: P1 " + commandSequenceP1.Count + " P2 " + commandSequenceP2.Count);
        StartCoroutine(sequentialAction(ePlayers.ONE, commandSequenceP1));
        StartCoroutine(sequentialAction(ePlayers.TWO, commandSequenceP2));
    }

    public IEnumerator sequentialAction(ePlayers playerId, List<eCommands> commandSequence){
        for(int i = 0; i < commandSequence.Count; i++) {
            yield return StartCoroutine(playerAction(playerId, commandSequence[i]));
        }
    }

    public IEnumerator playerAction(ePlayers player, eCommands command) {
        //determine player game object
        isPerformingAction = true;
        GameObject currentPlayer = player == ePlayers.ONE ? FindObjectOfType<GameManager>().getPlayerOne(): FindObjectOfType<GameManager>().getPlayerTwo();
        command = checkCommandValidity(player, command);
        Vector3 destinationPos = currentPlayer.transform.position;
        switch (command) {
            case eCommands.NONE:
                yield return new WaitForSeconds(1 / moveSpeed);
                break;
            case eCommands.UP:
                destinationPos += Vector3.forward;
                break;
            case eCommands.DOWN:
                destinationPos += Vector3.back;
                break;
            case eCommands.RIGHT:
                destinationPos += Vector3.right;
                break;
            case eCommands.LEFT:
                destinationPos += Vector3.left;
                break;
            default:
                Debug.LogError("PlayerMovement :: unknown Command");
                break;
        }

        while (currentPlayer.transform.position != destinationPos) {
            currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, destinationPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        for (int i = 0; i < 4; i++)
        {
            Debug.Log( i + " " + playerOneAvailableMovements[i]);
        }
        //Debug.Log(playerOneAvailableMovements);
        isPerformingAction = false;
        yield return new WaitForSeconds(movementDelay);
    //    Debug.Log("PlayerMovement :: heheheheheh");
        
    }

    public void modifyPlayerAvailableMovements(ePlayers player, int index, bool isAllowed)
    {
        bool[] arrayToModify = player == ePlayers.ONE ? playerOneAvailableMovements : playerTwoAvailableMovements;
        arrayToModify[index] = isAllowed;
    }


    public void resetPlayerPosition(Vector3 newPosP1,Vector3 newPosP2) {

    }

    public void stopRunningActions() {
        StopAllCoroutines();
    }

    private eCommands checkCommandValidity(ePlayers player, eCommands command)
    {
        bool[] boolList = player == ePlayers.ONE ? playerOneAvailableMovements : playerTwoAvailableMovements;
        if (command == eCommands.NONE)
        {
            return command;
        }
        int index = (int)command - 1;
        if(!boolList[index])
        {
            command = eCommands.NONE;
        }
        return command;
    }

    public void reInitAvailableMovements(ePlayers player)
    {
        bool[] boolList = player == ePlayers.ONE ? playerOneAvailableMovements : playerTwoAvailableMovements;

        for (int i = 0; i < 4; i++)
        {
            boolList[i] = true;
        }
    }

    public void reInitAvailableMovements()
    {
        for (int i = 0; i < 4; i++)
        {
            playerOneAvailableMovements[i] = true;
            playerTwoAvailableMovements[i] = true;
        }
    }
}
