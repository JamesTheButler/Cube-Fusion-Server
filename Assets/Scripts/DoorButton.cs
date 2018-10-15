using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {
    public GameObject door;
    private bool state;  //true if button is activated; false if else

	// TODO: do we need state?
	void Start () {
        state = false;
	}
	
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" || other.transform.parent.tag == "box") {
            hideDoor(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" || other.transform.parent.tag == "box") {
            hideDoor(false);
        }
    }

    private void hideDoor(bool isDoorHidden) {
        float yOffset = isDoorHidden ? -2f : 0f;
        door.transform.localPosition = new Vector3(door.transform.position.x, yOffset, door.transform.position.z);
    }
}
