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
    public bool[] playersFinishedTheirSequence = new bool[2];

    private void Start()
    {
        reInitAvailableMovements(ePlayers.ONE);
        reInitAvailableMovements(ePlayers.TWO);
        reInitPlayerFinishedSequence();
    }

    public void moveTwoPlayers(List<eCommands> commandSequenceP1, List<eCommands> commandSequenceP2) {
        StartCoroutine(sequentialAction(ePlayers.ONE, commandSequenceP1));
        StartCoroutine(sequentialAction(ePlayers.TWO, commandSequenceP2));
    }

    public IEnumerator sequentialAction(ePlayers playerId, List<eCommands> commandSequence){
        for(int i = 0; i < commandSequence.Count; i++) {
            if(playerId == ePlayers.ONE)
                Debug.Log(FindObjectOfType<GameManager>().isLevelCompleted+", "+commandSequence[i]);
            if (!FindObjectOfType<GameManager>().isLevelCompleted)
                yield return StartCoroutine(playerAction(playerId, commandSequence[i]));
            else
                break;
        }
        playersFinishedTheirSequence[(int) playerId] = true;
    }

    public IEnumerator playerAction(ePlayers player, eCommands command) {
        //determine player game object
        isPerformingAction = true;
        GameObject currentPlayer = player == ePlayers.ONE ? FindObjectOfType<GameManager>().getPlayerOne(): FindObjectOfType<GameManager>().getPlayerTwo();
        GameObject[] boxList = player == ePlayers.ONE ? boxesNextToPlayerOne : boxesNextToPlayerTwo;
        GameObject box = null;
        Transform model=currentPlayer.transform.Find("model");
        Vector3 boxNewPosition = new Vector3();
        command = checkCommandValidity(player, command);
        Vector3 destinationPos = currentPlayer.transform.position;
		Vector3 pointRotate=new Vector3(0,0,0);

        Vector3 axisRotation=new Vector3();
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
                axisRotation=Vector3.right;
                destinationPos += Vector3.forward;
                pointRotate=new Vector3(0f,-0.5f,0.5f);
                break;
            case eCommands.DOWN:
                if (boxList[(int)eCommands.DOWN - 1] != null)
                {
                    box = boxList[(int)eCommands.DOWN - 1];
                    boxNewPosition = box.transform.position + Vector3.back;
                }
                axisRotation=Vector3.left;
                destinationPos += Vector3.back;
                pointRotate=new Vector3(0f,-0.5f,-0.5f);
                break;
            case eCommands.RIGHT:
                if (boxList[(int)eCommands.RIGHT - 1] != null)
                {
                    box = boxList[(int)eCommands.RIGHT - 1];
                    boxNewPosition = box.transform.position + Vector3.right;
                }
                axisRotation=Vector3.back;
                destinationPos += Vector3.right;
                pointRotate=new Vector3(0.5f,-0.5f,0f);
                break;
            case eCommands.LEFT:
                if (boxList[(int)eCommands.LEFT - 1] != null)
                {
                    box = boxList[(int)eCommands.LEFT - 1];
                    boxNewPosition = box.transform.position + Vector3.left;
                }
                destinationPos += Vector3.left;
                axisRotation=Vector3.forward;
                pointRotate=new Vector3(-0.5f,-0.5f,0f);
                break;
            default:
                Debug.LogError("PlayerMovement :: unknown Command");
                break;
        }
        if (command != eCommands.NONE) {
            float angle = 0;
            Vector3 savePos = currentPlayer.transform.position;
            while (angle < 90) {
                float deltaAngle = 90 * Time.deltaTime * moveSpeed;
                model.RotateAround(currentPlayer.transform.position + pointRotate, axisRotation, deltaAngle);
                angle += deltaAngle;
                if (box != null) {
                    box.transform.position = Vector3.MoveTowards(box.transform.position, boxNewPosition, Time.deltaTime * moveSpeed);
                }

                yield return null;
            }
            currentPlayer.transform.position = destinationPos;
            FindObjectOfType<ParticlesManager>().playStepParticles(destinationPos);

            model.rotation = new Quaternion(0, 0, 0, 0);
            model.position = currentPlayer.transform.position;
        }
        isPerformingAction = false;
        yield return new WaitForSeconds(movementDelay);
    }

    public void modifyPlayerAvailableMovements(ePlayers player, int index, bool isAllowed) {
        bool[] arrayToModify = player == ePlayers.ONE ? playerOneAvailableMovements : playerTwoAvailableMovements;
        arrayToModify[index] = isAllowed;
    }

    public void modifyBoxesNextToPlayer(ePlayers player, int index, GameObject box)
    {
        GameObject[] arrayToModify = player == ePlayers.ONE ? boxesNextToPlayerOne : boxesNextToPlayerTwo;
        arrayToModify[index] = box;
    }
 
    public void stopRunningActions() {
        StopAllCoroutines();
    }

    private eCommands checkCommandValidity(ePlayers player, eCommands command)
    {
        if (command == eCommands.NONE) {
            return command;
        }

        bool[] boolList = player == ePlayers.ONE ? playerOneAvailableMovements : playerTwoAvailableMovements;
        int index = (int)command - 1;
        GameObject[] boxList = player == ePlayers.ONE ? boxesNextToPlayerOne : boxesNextToPlayerTwo;
        GameObject box = boxList[index];
       
        if (!boolList[index])
        {
            command = eCommands.NONE;
        }
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

    public void reInitPlayerFinishedSequence() {

        playersFinishedTheirSequence[0] = false;
        playersFinishedTheirSequence[1] = false;
    }
}
