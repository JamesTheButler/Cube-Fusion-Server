using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eCommands {
    NONE,
    DOWN,
    UP,
    RIGHT,
    LEFT
}

public enum ePlayers {
    ONE,
    TWO
}


public class GameManager : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;

    public GameObject nextLevelUI;
    public float fadeInTime;
    public float loadingScreenTime;
    public float fadeOutTime;
    public float waitTimeBeforeFade;
    public bool isLevelCompleted = false;
    public ServerManager sMng;
    LevelLoader lvlLoader;
    InputManager inputMgr;
    PlayerMovement playerMovement;

    void Start () {
        //init stuff
        lvlLoader = FindObjectOfType<LevelLoader>();
        lvlLoader.initiate();

        inputMgr = FindObjectOfType<InputManager>();
        inputMgr.reinit();

        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.reInitAvailableMovements();
        playerMovement.reInitBoxesNextToPlayer();

       // nextLevelUI.SetActive(false);
    }


    void Update()
    {
        //Checks if the players finished their movements
        if (playerMovement.playersFinishedTheirSequence[0] && playerMovement.playersFinishedTheirSequence[1] && !isLevelCompleted)
        {
            finishLevel(false);
            isLevelCompleted = false;
            playerMovement.reInitPlayerFinishedSequence();
        }
    }

    public void finishLevel(bool hasSucceeded) {          
        switchPlayerColliders(false);
        inputMgr.reinit();

        if (hasSucceeded) {            // level completed
            isLevelCompleted = true;
            StartCoroutine(levelTransition(false));
        } else {                       // level failed
            StartCoroutine(levelTransition(true));
        }
    }

    private IEnumerator levelTransition(bool doRestartLevel) {
        //wait before fading, so players see the result
        yield return new WaitForSeconds(waitTimeBeforeFade);

        Debug.Log(doRestartLevel);
        // set text to show
        if (doRestartLevel)
            nextLevelUI.GetComponentInChildren<Text>().text = "Ouch! That didn't work. You'll have to try again ...";
        else
            nextLevelUI.GetComponentInChildren<Text>().text = "Good job, you did it! Loading next level ...";

        // init UI fading
        nextLevelUI.GetComponent<CanvasGroup>().alpha = 0f;

        //fade in ui
        while (nextLevelUI.GetComponent<CanvasGroup>().alpha < 1f) {
            nextLevelUI.GetComponent<CanvasGroup>().alpha += Time.deltaTime / fadeInTime;
            yield return null;
        }
        yield return new WaitForSeconds(loadingScreenTime);

        // load level
        if (doRestartLevel)
            lvlLoader.reloadLevel();
        else
            lvlLoader.loadNextLevel();

        // init level
        switchPlayerColliders(true);
        isLevelCompleted = false;

        //fade out
        nextLevelUI.GetComponent<CanvasGroup>().alpha = 1f;
        while (nextLevelUI.GetComponent<CanvasGroup>().alpha > 0f) {
            nextLevelUI.GetComponent<CanvasGroup>().alpha -= Time.deltaTime / fadeInTime;
            yield return null;
        }
        sMng.PlayersReady(!doRestartLevel);
    }

    public void switchPlayerColliders(bool on) {
        foreach (BoxCollider c in playerOne.gameObject.GetComponents<BoxCollider>())
            c.enabled = on;
        foreach (BoxCollider c in playerTwo.gameObject.GetComponents<BoxCollider>())
            c.enabled = on;
    }

    public GameObject getPlayerOne()
    {
        return playerOne;
    }

    public GameObject getPlayerTwo()
    {
        return playerTwo;
    }

}
