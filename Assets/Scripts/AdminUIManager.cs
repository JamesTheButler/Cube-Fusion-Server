using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminUIManager : MonoBehaviour {
    public LevelLoader levelLoader;
    public GameObject levelSelectionMenu;
    public GameObject levelButtonPrefab;

    public int levelButtonSize;
    public int levelButtonMargin;
    private int buttonsPerLine;

    public Image playerOneIsConnectedImage;
    public Image playerTwoIsConnectedImage;


    private void Start() {
        createLevelSelectionMenu();
        setPlayerIsConnected(ePlayers.ONE, false);
        setPlayerIsConnected(ePlayers.TWO, false);
    }

    private void createLevelSelectionMenu() {
        buttonsPerLine = Screen.width / (levelButtonSize + levelButtonMargin);
        for(int i =0; i< levelLoader.levels.Length; i++) {
            GameObject lvlBtn = Instantiate(levelButtonPrefab, levelSelectionMenu.transform);
            lvlBtn.GetComponent<LevelSelectionButton>().setup(levelLoader, i);
            lvlBtn.GetComponentInChildren<Text>().text = ""+(i+1);
            Vector3 position = new Vector3(i%buttonsPerLine * (levelButtonMargin + levelButtonSize), -(i/buttonsPerLine) * (levelButtonMargin + levelButtonSize), 0);
            ((RectTransform)lvlBtn.transform).localPosition = position;
        }
    }

    public  void setPlayerIsConnected(ePlayers player, bool isConnected) {
        Image img = player == ePlayers.ONE ? playerOneIsConnectedImage : playerTwoIsConnectedImage;
        if (isConnected)
            img.color = Color.green;
        else
            img.color = Color.red;

    }
}
