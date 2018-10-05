using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float nextLevelLoadingScreenTime;

    LevelLoader lvlLoader;
    InputManager inputMgr;

    void Start () {
        //init stuff
        lvlLoader = FindObjectOfType<LevelLoader>();
        lvlLoader.initiate();

        inputMgr = FindObjectOfType<InputManager>();
        inputMgr.reinit();

        nextLevelUI.SetActive(false);
    }

    public void restartLevel() {
        inputMgr.reinit();
        lvlLoader.reloadLevel();

    }
    
    public void transitionToNextLevel() {
        // disable collisions between players
        switchPlayerColliders(false);
        // show level changing screen
        nextLevelUI.SetActive(true);
        // reinit input and movement method
        inputMgr.reinit();
        // load next level into scene
        lvlLoader.loadNextLevel();
        // disable loading screen after certain time
        Invoke("hideNextLevelLoadingScreen", nextLevelLoadingScreenTime);
    }

    void hideNextLevelLoadingScreen() {
        nextLevelUI.SetActive(false);
        switchPlayerColliders(true);
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
