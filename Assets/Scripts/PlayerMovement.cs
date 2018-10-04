using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 3.0f;

    public IEnumerator movePlayer(int playerId, List<int> movements){
        GameObject currentPlayer = playerId == GameManager.PLAYER_ONE_ID ? FindObjectOfType<GameManager>().getPlayerOne() : FindObjectOfType<GameManager>().getPlayerTwo();
        Vector3 destinationPos = currentPlayer.transform.position;

        for(int i = 0; i < movements.Count; i++)
        {
            int currentMove = movements[i];
            switch (currentMove)
            {
                case GameManager.UP:
                    destinationPos += Vector3.forward;
                    break;
                case GameManager.DOWN:
                    destinationPos += Vector3.back;
                    break;
                case GameManager.LEFT:
                    destinationPos += Vector3.left;
                    break;
                case GameManager.RIGHT:
                    destinationPos += Vector3.right;
                    break;
            }
            while(currentPlayer.transform.position != destinationPos)
            {
                currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, destinationPos, Time.deltaTime * moveSpeed);
                yield return null;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void movePlayers(List<int> movementsP1, List<int> movementsP2)
    {
        Debug.Log(" P1 " + movementsP1.Count + " P2 " + movementsP2.Count);
        StartCoroutine(movePlayer(GameManager.PLAYER_ONE_ID, movementsP1));
        Debug.Log("Player two movements");
        StartCoroutine(movePlayer(GameManager.PLAYER_TWO_ID, movementsP2));
    }    
}
