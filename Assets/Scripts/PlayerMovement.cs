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
    public GameObject[] boxesNextToPlayerOne = new GameObject[4];//Down - Up - Right - Left
    public GameObject[] boxesNextToPlayerTwo = new GameObject[4];//Down - Up - Right - Left

    private void Start()
    {
        reInitAvailableMovements(ePlayers.ONE);
        reInitAvailableMovements(ePlayers.TWO);
    }

    public void moveTwoPlayers(List<eCommands> commandSequenceP1, List<eCommands> commandSequenceP2) {
        Debug.Log("PlayerMovement :: P1 " + commandSequenceP1.Count + " P2 " + commandSequenceP2.Count);
        string p1Output = "", p2Output="";
        foreach (eCommands cmd in commandSequenceP1)
            p1Output += cmd + ", ";
        foreach (eCommands cmd in commandSequenceP2)
            p2Output += cmd + ", ";
        Debug.Log(p1Output);
        Debug.Log(p2Output);
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
        GameObject[] boxList = player == ePlayers.ONE ? boxesNextToPlayerOne : boxesNextToPlayerTwo;
        GameObject box = null;
        Vector3 boxNewPosition = new Vector3();
        command = checkCommandValidity(player, command);
        Vector3 destinationPos = currentPlayer.transform.position;
        switch (command)
        {
            case eCommands.NONE:
                yield return new WaitForSeconds(1 / moveSpeed);
                break;
            case eCommands.UP:
                if (boxList[(int)eCommands.UP - 1] != null)
                {
                    box = boxList[(int)eCommands.UP - 1];
                    boxNewPosition = box.transform.position + Vector3.forward;
                }     
                destinationPos += Vector3.forward;
                break;
            case eCommands.DOWN:
                if (boxList[(int)eCommands.DOWN - 1] != null)
                {
                    box = boxList[(int)eCommands.DOWN - 1];
                    boxNewPosition = box.transform.position + Vector3.back;
                }
                destinationPos += Vector3.back;
                break;
            case eCommands.RIGHT:
                if (boxList[(int)eCommands.RIGHT - 1] != null)
                {
                    box = boxList[(int)eCommands.RIGHT - 1];
                    boxNewPosition = box.transform.position + Vector3.right;
                }
                destinationPos += Vector3.right;
                break;
            case eCommands.LEFT:
                if (boxList[(int)eCommands.LEFT - 1] != null)
                {
                    box = boxList[(int)eCommands.LEFT - 1];
                    boxNewPosition = box.transform.position + Vector3.left;
                }
                destinationPos += Vector3.left;
                break;
            default:
                Debug.LogError("PlayerMovement :: unknown Command");
                break;
        }

        while (currentPlayer.transform.position != destinationPos) {
            currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, destinationPos, Time.deltaTime * moveSpeed);
            if (box != null)
            {
                box.transform.position = Vector3.MoveTowards(box.transform.position, boxNewPosition, Time.deltaTime * moveSpeed);
            }
            yield return null;
        }
     /*   for (int i = 0; i < 4; i++)
        {
            Debug.Log( i + " " + playerOneAvailableMovements[i]);
        }*/
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

    public void modifyBoxesNextToPlayer(ePlayers player, int index, GameObject box)
    {
        GameObject[] arrayToModify = player == ePlayers.ONE ? boxesNextToPlayerOne : boxesNextToPlayerTwo;
        arrayToModify[index] = box;
    }


    public void resetPlayerPosition(Vector3 newPosP1,Vector3 newPosP2) {

    }

    public void stopRunningActions() {
        StopAllCoroutines();
    }

    private eCommands checkCommandValidity(ePlayers player, eCommands command)
    {
        bool[] boolList = player == ePlayers.ONE ? playerOneAvailableMovements : playerTwoAvailableMovements;
        int index = (int)command - 1;
        GameObject[] boxList = player == ePlayers.ONE ? boxesNextToPlayerOne : boxesNextToPlayerTwo;
        GameObject box = boxList[index];
        if (command == eCommands.NONE)
        {
            return command;
        }
        if (!boolList[index])
        {
            command = eCommands.NONE;
        }
        //TODO merge to the other if, if working properly
        else if (boxList[index] != null && !box.GetComponent<BoxInteractions>().canMoveToDirection(command))
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

    public void reInitBoxesNextToPlayer()
    {
        for(int i = 0; i < 4; i++)
        {
            boxesNextToPlayerOne[i] = null;
            boxesNextToPlayerTwo[i] = null;
        }
    }
}
