using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {
    public GameObject door;
    private bool doorIsAlreadyOpen;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" || other.transform.parent.tag == "box" && !doorIsAlreadyOpen) {
            StartCoroutine(hideDoor(true));
            doorIsAlreadyOpen = true;
            
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" || other.transform.parent.tag == "box" && doorIsAlreadyOpen) {
            StartCoroutine(hideDoor(false));
            doorIsAlreadyOpen = false;
        }
    }

    private IEnumerator hideDoor(bool isDoorHidden) {
        float yOffset = isDoorHidden ? -0.77f : 0.33f;
        Vector3 newPos = new Vector3(door.transform.position.x, yOffset, door.transform.position.z);
        
        //door.transform.localPosition = new Vector3(door.transform.position.x, yOffset, door.transform.position.z);
        while (door.transform.localPosition != newPos)
        {
            
            door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, newPos, Time.deltaTime * FindObjectOfType<PlayerMovement>().moveSpeed);
            yield return null;
        }
    }
}
