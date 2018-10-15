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

    LevelLoader lvlLoader;
    InputManager inputMgr;

    void Start () {
        //init stuff
        lvlLoader = FindObjectOfType<LevelLoader>();
        lvlLoader.initiate();

        inputMgr = FindObjectOfType<InputManager>();
        inputMgr.reinit();
    }

    public void restartLevel() {
        Debug.Log("restart lvl");
        inputMgr.reinit();
        lvlLoader.reloadLevel();
    }

    public void finishLevel(bool isSucceeded) {
        if (isSucceeded) {                      // level completed
            Debug.Log("GameManager :: Level succesfully completed");
            // disable collisions between players
            switchPlayerColliders(false);
            // reinit input and movement method
            inputMgr.reinit();
            // start transition to show next level
            StartCoroutine(levelTransition());
        } else {                            // level failed

        }
    }

    private IEnumerator levelTransition() {
        yield return new WaitForSeconds(waitTimeBeforeFade);
        // init UI fading
        float alpha = 0f;
        Color textColor = nextLevelUI.GetComponentInChildren<Text>().color;
        if (textColor == null)
            Debug.LogError("color empty");
        Color backGroundColor = nextLevelUI.GetComponentInChildren<Image>().color;

        // make ui invisible
        textColor.a = alpha;
        backGroundColor.a = alpha;

        //fade in ui
        while (alpha < 1f) {
            alpha += Time.deltaTime / fadeInTime;
            textColor.a = alpha;
            backGroundColor.a = alpha;

            nextLevelUI.GetComponentInChildren<Text>().color = textColor;
            nextLevelUI.GetComponentInChildren<Image>().color = backGroundColor;
            yield return null;
        }
        //show ui for certain time
        yield return new WaitForSeconds(loadingScreenTime);
        //nextLevelUI.SetActive(false);
        switchPlayerColliders(true);
        lvlLoader.loadNextLevel();
        GetComponent<PlayerMovement>().reInitAvailableMovements();

        //set alpha to 1 for safety
        alpha = 1f;
        //fade out
        while (alpha > 0f) {
            alpha -= Time.deltaTime / fadeInTime;
            textColor.a = alpha;
            backGroundColor.a = alpha;

            nextLevelUI.GetComponentInChildren<Text>().color = textColor;
            nextLevelUI.GetComponentInChildren<Image>().color = backGroundColor;
            yield return null;
        }
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
