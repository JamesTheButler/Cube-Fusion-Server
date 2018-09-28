using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;
    
    public GameObject nextLevelUI;
    public float nextLevelLoadingScreenTime;

    LevelLoader lvlLoader;
    
    // Use this for initialization
    void Start () {
        //init stuff
        lvlLoader = FindObjectOfType<LevelLoader>();
        lvlLoader.initiate();
        
        nextLevelUI.SetActive(false);
    }

    public void onLevelCompleted() {
        transitionToNextLevel();
    }

    public void transitionToNextLevel() {
        nextLevelUI.SetActive(true);

        switchPlayerColliders(false);
        lvlLoader.loadNextLevel();
        Invoke("hideNextLevelLoadingScreen", nextLevelLoadingScreenTime);
    }

    void hideNextLevelLoadingScreen() {
        nextLevelUI.SetActive(false);

        switchPlayerColliders(true);
    }

    public void switchPlayerColliders(bool on) {
        playerOne.gameObject.GetComponent<BoxCollider>().enabled = on;
        playerTwo.gameObject.GetComponent<BoxCollider>().enabled = on;
    }

}
