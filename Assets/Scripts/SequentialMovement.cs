using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialMovement : MonoBehaviour {

    PlayerMovement playerMovement;
 //   private const float  TIME_TO_WAIT_BETWEEN_INPUTS = 0.2f;

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
      //  Debug.Log("Waiting for Input");
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            // Player One Movement
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_ONE_ID, GameManager.RIGHT));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_ONE_ID, GameManager.LEFT));
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_ONE_ID, GameManager.UP));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_ONE_ID, GameManager.DOWN));
            }
            
            //Player two movement
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_TWO_ID, GameManager.RIGHT));
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_TWO_ID, GameManager.LEFT));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_TWO_ID, GameManager.UP));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                yield return StartCoroutine(registerInput(GameManager.PLAYER_TWO_ID, GameManager.DOWN));
            }
            yield return null;
            
        }

        Debug.Log("Starting movements");
        playerMovement.movePlayers(playerOneMovement, playerTwoMovement);
    }


    private IEnumerator registerInput(int idPlayer, int direction)
    {
        List<int> listToAddInput = idPlayer == GameManager.PLAYER_ONE_ID ? playerOneMovement : playerTwoMovement;
        listToAddInput.Add(direction);
        //    yield return new WaitForSeconds(TIME_TO_WAIT_BETWEEN_INPUTS);
        yield return null;
        Debug.Log("Waiting for a new Input");
    }
}
