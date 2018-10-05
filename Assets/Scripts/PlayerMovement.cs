using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 3.0f;
    public float movementDelay = 0.1f;
    [HideInInspector]
    public bool isPerformingAction = false;

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

        Vector3 destinationPos = currentPlayer.transform.position;
        switch (command) {
            case eCommands.NONE:
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
        yield return new WaitForSeconds(movementDelay);
    //    Debug.Log("PlayerMovement :: heheheheheh");
        isPerformingAction = false;
    }


    public void resetPlayerPosition(Vector3 newPosP1,Vector3 newPosP2) {

    }

    public void stopRunningActions() {
        StopAllCoroutines();
    }
}
