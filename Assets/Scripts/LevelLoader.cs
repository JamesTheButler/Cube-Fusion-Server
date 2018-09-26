using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    public GameObject[] levels;
    private string currentLevelTag = "currentLevel";
    
    public void loadLevel(int id) {
        if (levels.Length < id)
            return;

        unloadCurrentLevel();
        levels[id].tag = currentLevelTag;
        Instantiate(levels[id]);
    }

    public void unloadCurrentLevel() {
        GameObject currentLevel = GameObject.FindGameObjectWithTag(currentLevelTag);
        if(currentLevel != null)
            Destroy(currentLevel);
    }
}
