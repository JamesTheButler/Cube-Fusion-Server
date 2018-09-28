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


        /*     GameObject go = GameObject.FindGameObjectWithTag("startPlayerOne");
             Debug.Log(go.transform.position.x);
             Vector3 test = go.transform.position;
             test.x += 1;
             go.transform.position = test;
             Debug.Log(go.transform.position.x);
             */
        Debug.Log("playerone start positins: " + GameObject.FindGameObjectsWithTag("startPlayerOne").Length);
        Vector3 playerOneStartPos = GameObject.FindGameObjectWithTag("startPlayerOne").transform.position;
        playerOne.GetComponent<PlayerOneMovement>().setPos(playerOneStartPos);
        
        Vector3 playerTwoStartPos = GameObject.FindGameObjectWithTag("startPlayerTwo").transform.position;
        playerTwo.GetComponent<PlayerTwoMovement>().setPos(playerTwoStartPos);

        Debug.Log("p1 pos: " + playerOneStartPos);

        SequentialMovement sequentialMovement = SequentialMovement.getInstance();
        StopCoroutine(sequentialMovement.ReadInputs());
        StartCoroutine(sequentialMovement.ReadInputs());
    }

    public void unloadCurrentLevel() {
        GameObject currentLevel = GameObject.FindGameObjectWithTag(currentLevelTag);

        GameObject.FindGameObjectWithTag("startPlayerOne").tag = "Untagged";
        GameObject.FindGameObjectWithTag("startPlayerTwo").tag = "Untagged";
        if (currentLevel != null)
            Destroy(currentLevel);
    }
}
