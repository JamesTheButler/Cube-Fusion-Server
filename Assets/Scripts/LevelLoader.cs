using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;

    public GameObject[] levels;
    private string currentLevelTag = "currentLevel";

    private void Start() {
        loadLevel(0);
    }

    public void loadLevel(int id) {
        if (levels.Length < id)
            return;
        //remove currently loaded level
        unloadCurrentLevel();
        //load next level
        levels[id].tag = currentLevelTag;
        Instantiate(levels[id]);
        //place players
        playerOne.transform.position = GameObject.FindGameObjectWithTag("startPlayerOne").transform.position;
        playerTwo.transform.position = GameObject.FindGameObjectWithTag("startPlayerTwo").transform.position;
    }

    public void unloadCurrentLevel() {
        GameObject currentLevel = GameObject.FindGameObjectWithTag(currentLevelTag);
        if(currentLevel != null)
            Destroy(currentLevel);
    }
}
