using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialMovement {

    
    private static SequentialMovement instance = null;
    enum Direction { Left, Right, Up, Down };
    //public GameObject playerOne;
    List<Direction> playerOneMovement = new List<Direction>();
    //public GameObject playerTwo;
    List<Direction> playerTwoMovement = new List<Direction>();

    public static SequentialMovement getInstance()
    {
        if (instance == null)
        {
            instance = new SequentialMovement();
        }

        return instance;
    }

    //Simulates the creation of a sequence (by pressing buttons wasd)
    //Then when the user presses return : reads the inputs in the console
    public IEnumerator ReadInputs()
    {
        playerOneMovement.Clear();
        playerTwoMovement.Clear();
        Debug.Log("Waiting for Input");
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            

            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(Direction.Right);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Waiting for a new Input");
                
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(Direction.Left);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(Direction.Up);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Waiting for a new Input");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("A key has been pressed");
                playerOneMovement.Add(Direction.Down);
                Debug.Log("Wait please");
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Waiting for a new Input");
            }
            yield return null;
            
        }
        Debug.Log("Movement inputs : ");
        for (int i = 0; i < playerOneMovement.Count; i++)
        {
            Debug.Log(playerOneMovement[i]);  
        }
        yield break;

    }


    /*public void setPlayers(GameObject player1, GameObject player2)
    {
        playerOne = player1;
        playerTwo = player2;
    }*/


}
