using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour {
    LevelLoader levelLoader;
    int levelId;

    public void setup(LevelLoader levelLoader, int levelId) {
        this.levelLoader = levelLoader;
        this.levelId = levelId;
    }

    public void selectLevel() {
        levelLoader.loadLevel(levelId);
    }
}
