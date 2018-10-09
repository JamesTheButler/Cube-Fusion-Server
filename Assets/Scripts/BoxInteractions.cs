using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractions : MonoBehaviour {

    public bool[] wallsNextToBox = new bool[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform.parent.tag == "wall" || other.transform.parent.tag == "Player")
        {
            Debug.Log("Box found a wall");
            double diffX = other.transform.position.x - this.transform.position.x;
            double checkX = (other.transform.lossyScale.x + this.transform.lossyScale.x) / 2;
            double diffZ = other.transform.position.z - this.transform.position.z;
            double checkZ = (other.transform.lossyScale.z + this.transform.lossyScale.z) / 2;
            if (diffX == checkX)
            {
                wallsNextToBox[(int)eCommands.RIGHT - 1] = true;
            }
            if (diffX == -checkX)
            {
                wallsNextToBox[(int)eCommands.LEFT - 1] = true;
            }
            if (diffZ == checkZ)
            {
                wallsNextToBox[(int)eCommands.UP - 1] = true;
            }
            if (diffZ == -checkZ)
            {
                wallsNextToBox[(int)eCommands.DOWN - 1] = true;
            }
        }
    }


    public bool canMoveToDirection(eCommands command)
    {
        return !wallsNextToBox[(int)command - 1];
    }

    private void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < 4; i++)
        {
            wallsNextToBox[i] = false;
        }
    }



}
