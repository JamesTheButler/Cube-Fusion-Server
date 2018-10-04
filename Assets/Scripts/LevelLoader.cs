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
            Debug.LogError("Couldn't load level. Level ID does not exist");
            return;
        }
        currentLevelID = id;

        //remove currently loaded level
        unloadCurrentLevel();
        
        //load next level
        levels[id].tag = CURRENT_LEVEL_TAG;
        Instantiate(levels[id], scene.transform);
        
        //place players
        Vector3 playerOneStartPos = GameObject.FindGameObjectWithTag("startPlayerOne").transform.position;
        playerOne.transform.position = playerOneStartPos;
        playerOne.GetComponent<PlayerOneMovement>().setPos(playerOneStartPos);
        Vector3 playerTwoStartPos = GameObject.FindGameObjectWithTag("startPlayerTwo").transform.position;
        playerTwo.transform.position = playerTwoStartPos;
        playerTwo.GetComponent<PlayerTwoMovement>().setPos(playerTwoStartPos);

        levelIdText.text = "Level " + (id+1);

        Debug.Log("p1 pos: " + playerOneStartPos);

        SequentialMovement sequentialMovement = FindObjectOfType<SequentialMovement>();
        StopCoroutine(sequentialMovement.ReadInputs());
        StartCoroutine(sequentialMovement.ReadInputs());
    }

    public void unloadCurrentLevel() {
        Debug.Log("unloading current lvl");
        GameObject currentLevel = GameObject.FindGameObjectWithTag(CURRENT_LEVEL_TAG);

        if(GameObject.FindGameObjectWithTag(PLAYER_ONE_TAG) != null)
            GameObject.FindGameObjectWithTag(PLAYER_ONE_TAG).tag = "Untagged";

        if (GameObject.FindGameObjectWithTag(PLAYER_TWO_TAG) != null)
            GameObject.FindGameObjectWithTag(PLAYER_TWO_TAG).tag = "Untagged";

        if (currentLevel != null)
            Destroy(currentLevel);
    }

    public void loadNextLevel() {
        loadLevel((currentLevelID+1) % levels.Length);
    }
}
