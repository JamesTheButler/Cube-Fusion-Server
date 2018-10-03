using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float speed = 3.0f;

    // Use this for initialization
    void Start ()
    {


    }

    public IEnumerator movePlayer(List<int> movements)
    {
        //GameObject currentPlayer = idPlayer == GameManager.PLAYER_ONE_ID ? playerOne : playerTwo;
        GameObject playerOne = FindObjectOfType<GameManager>().getPlayerOne();
        Vector3 posP1 = playerOne.transform.position;

        for(int i = 0; i < movements.Count; i++)
        {
            int currentMove = movements[i];
            switch (currentMove)
            {
                case GameManager.UP:
                    posP1 += Vector3.forward;
                    break;
                case GameManager.DOWN:
                    posP1 += Vector3.back;
                    break;
                case GameManager.LEFT:
                    posP1 += Vector3.left;
                    break;
                case GameManager.RIGHT:
                    posP1 += Vector3.right;
                    break;
            }
            while(playerOne.transform.position != posP1)
            {
                playerOne.transform.position = Vector3.MoveTowards(playerOne.transform.position, posP1, Time.deltaTime * speed);
                yield return null;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    
}
