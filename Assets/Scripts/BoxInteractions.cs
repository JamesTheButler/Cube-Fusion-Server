using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractions : MonoBehaviour {

    public bool[] somethingIsNextToBox = new bool[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.tag == "wall" || other.transform.parent.tag == "Player" || other.transform.parent.tag == "box")
        {
            double diffX = other.transform.position.x - this.transform.position.x;
            double checkX = (other.transform.lossyScale.x + this.transform.lossyScale.x) / 2;
            double diffZ = other.transform.position.z - this.transform.position.z;
            double checkZ = (other.transform.lossyScale.z + this.transform.lossyScale.z) / 2;
            if (diffX == checkX)
            {
                somethingIsNextToBox[(int)eCommands.RIGHT - 1] = true;
            }
            if (diffX == -checkX)
            {
                somethingIsNextToBox[(int)eCommands.LEFT - 1] = true;
            }
            if (diffZ == checkZ)
            {
                somethingIsNextToBox[(int)eCommands.UP - 1] = true;
            }
            if (diffZ == -checkZ)
            {
                somethingIsNextToBox[(int)eCommands.DOWN - 1] = true;
            }
        }
    }


    public bool canMoveToDirection(eCommands command)
    {
        return !somethingIsNextToBox[(int)command - 1];
    }

    private void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < 4; i++)
        {
            somethingIsNextToBox[i] = false;
        }
    }



}
