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
    public GameObject endOfGameUI;
    public float fadeInTime;
    public float loadingScreenTime;
    public float fadeOutTime;
    public float waitTimeBeforeFade;
    public float endGameTime;
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

        nextLevelUI.SetActive(false);
        endOfGameUI.SetActive(false);
    }

    void Update()
    {
        //Checks if the players finished their movements
        if (playerMovement.playersFinishedTheirSequence[0] && playerMovement.playersFinishedTheirSequence[1] && !isLevelCompleted)
        {
            FindObjectOfType<ParticlesManager>().playLosingParticles(playerOne.transform.position + new Vector3(0f,1f));
            FindObjectOfType<ParticlesManager>().playLosingParticles(playerTwo.transform.position + new Vector3(0f,1f));
            finishLevel(false);
            playerMovement.reInitPlayerFinishedSequence();
        }
    }

    public void finishLevel(bool hasSucceeded) {
        isLevelCompleted = hasSucceeded;
        Debug.Log("GameManager :: finished level. success status: " + hasSucceeded);
        switchPlayerColliders(false);
        inputMgr.reinit();
        LevelLoader lvlLoader = FindObjectOfType<LevelLoader>();
        if(hasSucceeded && lvlLoader.isCurrentLvlTheLastOne())      //end of game
            StartCoroutine(showEndGameUI());
        else if (hasSucceeded && !lvlLoader.isCurrentLvlTheLastOne()) {    // level completed
            StartCoroutine(levelTransition(false));
        } else if(!hasSucceeded) {                       // level failed
            StartCoroutine(levelTransition(true));
        }
    }

    private IEnumerator levelTransition(bool doRestartLevel) {
        Debug.Log("levelTransitionI()");
        if (!(!doRestartLevel && lvlLoader.isCurrentLvlTheLastOne()))
            yield return new WaitForSeconds(waitTimeBeforeFade);
        nextLevelUI.SetActive(true);
        nextLevelUI.GetComponentInChildren<Image>().color = doRestartLevel ? Color.black : Color.white;
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

    public IEnumerator showEndGameUI() {
        endOfGameUI.SetActive(true);
        endOfGameUI.GetComponent<CanvasGroup>().alpha = 0f;
        while (endOfGameUI.GetComponent<CanvasGroup>().alpha < 1f) {
            endOfGameUI.GetComponent<CanvasGroup>().alpha += Time.deltaTime / fadeInTime;
            yield return null;
        }
        Debug.Log("Game has now finished");
        yield return new WaitForSeconds(endGameTime);
        endOfGameUI.GetComponent<CanvasGroup>().alpha = 1f;
        while (endOfGameUI.GetComponent<CanvasGroup>().alpha > 0f) {
            endOfGameUI.GetComponent<CanvasGroup>().alpha -= Time.deltaTime / fadeOutTime;
            yield return null;
        }
        endOfGameUI.SetActive(false);
        Debug.Log("game manager : start transition");
        yield return StartCoroutine(levelTransition(false));
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
