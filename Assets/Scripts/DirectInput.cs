using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectInput : MonoBehaviour {

    public eCommands readInput(ePlayers player) {
        if (player == ePlayers.ONE) {
            if (Input.GetKey(KeyCode.D)) {
                return eCommands.RIGHT;
            } else if (Input.GetKey(KeyCode.A)) {
                return eCommands.LEFT;
            } else if (Input.GetKey(KeyCode.W)) {
                return eCommands.UP;
            } else if (Input.GetKey(KeyCode.S)) {
                return eCommands.DOWN;
            } else
                return eCommands.NONE;
        } else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                return eCommands.RIGHT;
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                return eCommands.LEFT;
            } else if (Input.GetKey(KeyCode.UpArrow)) {
                return eCommands.UP;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                return eCommands.DOWN;
            } else
                return eCommands.NONE;
        }
    }
}
