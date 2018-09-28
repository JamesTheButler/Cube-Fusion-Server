using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    GameManager gameMgr;

    private void Start() {
        gameMgr = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("The Players have met");
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            gameMgr.onLevelCompleted();
        }
    }

}
