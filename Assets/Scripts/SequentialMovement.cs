using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialMovement : MonoBehaviour {

    PlayerMovement playerMovement;

    //public GameObject playerOne;
    List<int> playerOneMovement = new List<int>();
    //public GameObject playerTwo;
    List<int> playerTwoMovement = new List<int>();


    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        Debug.Log(playerMovement.tag);
    }

    //Simulates the creation of a sequence (by pressing buttons wasd)
    //Then when the user presses return : reads the inputs in the console
    public IEnumerator ReadInputs()
    {
        yield return null;
        playerOneMovement.Clear();
        playerTwoMovement.Clear();
        Debug.Log("Waiting for Input");
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(GameManager.RIGHT);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Waiting for a new Input");
                
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(GameManager.LEFT);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(GameManager.UP);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Waiting for a new Input");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(GameManager.DOWN);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Waiting for a new Input");
            }
            yield return null;
            
        }

        Debug.Log("Starting movements");
        yield return StartCoroutine(playerMovement.movePlayer(playerOneMovement));
        Debug.Log("Finished Movements");
        yield break;

    }


    /*public void setPlayers(GameObject player1, GameObject player2)
    {
        playerOne = player1;
        playerTwo = player2;
    }*/


}
