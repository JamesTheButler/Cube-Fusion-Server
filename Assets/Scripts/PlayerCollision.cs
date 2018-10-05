using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    public GameObject gameMgr;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject != this.gameObject) {
          //  Debug.Log(gameMgr.GetComponent<PlayerMovement>().isPerformingAction);

            if (!gameMgr.GetComponent<PlayerMovement>().isPerformingAction) {
                if (other.tag == "Player") {
                    gameMgr.GetComponent<GameManager>().transitionToNextLevel();
                }
            }
        }
    }
}
