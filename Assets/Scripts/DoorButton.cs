using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {
    public GameObject door;
    private bool state;  //true if button is activated; false if else

	// Use this for initialization
	void Start () {
        state = false;
	}
	
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            hideDoor(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            hideDoor(false);
        }
    }

    private void hideDoor(bool isDoorHidden) {
        float yOffset = isDoorHidden ? -5f : 5f;
        door.transform.position += new Vector3(0,yOffset,0);
        //door.SetActive(!isDoorHidden);
    }
}
