using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject scene;

    public GameObject[] levels;

    public Text levelIdText;

    private const string CURRENT_LEVEL_TAG = "currentLevel";
    private const string PLAYER_ONE_TAG = "startPlayerOne";
    private const string PLAYER_TWO_TAG = "startPlayerTwo";
    private int currentLevelID;
    
    public void initiate(){

        currentLevelID = 0;
        loadLevel(currentLevelID);
    }

    public void loadLevel(int id) {
        if (levels.Length < id) {
            Debug.LogError("LevelLoader :: Couldn't load level. Level ID does not exist");
            return;
        }
        currentLevelID = id;
        Debug.Log("LevelLoader :: loading level " + id);

        //Stop movements
        FindObjectOfType<PlayerMovement>().stopRunningActions();

        //remove currently loaded level
        unloadOldLevel();

        //load next level
        levels[id].tag = CURRENT_LEVEL_TAG;
        Instantiate(levels[id], scene.transform);

        Vector3 playerOneStartPos = GameObject.FindGameObjectWithTag("startPlayerOne").transform.position;
        Vector3 playerTwoStartPos = GameObject.FindGameObjectWithTag("startPlayerTwo").transform.position;
        playerOne.transform.position = playerOneStartPos;
        playerTwo.transform.position = playerTwoStartPos;

        levelIdText.text = "Level " + (id+1);
    }

    public void unloadOldLevel() {
        if(GameObject.FindGameObjectWithTag(PLAYER_ONE_TAG) != null)
            GameObject.FindGameObjectWithTag(PLAYER_ONE_TAG).tag = "Untagged";

        if (GameObject.FindGameObjectWithTag(PLAYER_TWO_TAG) != null)
            GameObject.FindGameObjectWithTag(PLAYER_TWO_TAG).tag = "Untagged";
      
        // delete old level. For safety delete all objects with the current level tag
        GameObject[] loadedLevels = GameObject.FindGameObjectsWithTag(CURRENT_LEVEL_TAG);
        for (int i =0; i < loadedLevels.Length; i++) {
            Destroy(loadedLevels[i]);
        }
    }

    public void reloadLevel() {
        loadLevel(currentLevelID);
    }

    public void loadNextLevel() {
        loadLevel((currentLevelID+1) % levels.Length);
    }
}
