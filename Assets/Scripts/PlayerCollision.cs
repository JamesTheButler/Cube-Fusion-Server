using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameMgr;
    public bool isPlayerOne;

    private void OnTriggerStay(Collider other) {
        PlayerMovement playerMovement = gameMgr.GetComponent<PlayerMovement>();

        if (isPlayerOne && other.gameObject != this.gameObject)
        {
            if (!playerMovement.isPerformingAction)
            {
                if (other.tag == "Player")
                {
                    gameMgr.GetComponent<GameManager>().transitionToNextLevel();
                }
            }
        }
        setAvailableMovements(other);
    }

    private void OnTriggerExit(Collider other)
    {
        gameMgr.GetComponent<PlayerMovement>().reInitAvailableMovements();
    }

    /**
     * Completely clean code, nothing to see here
     * */
    private void setAvailableMovements(Collider other)
    {
        PlayerMovement playerMovement = gameMgr.GetComponent<PlayerMovement>();
        ePlayers player = this.name == "Player 1" ? ePlayers.ONE : ePlayers.TWO;
        if (other.transform.parent.tag == "wall")
        {
            double diffX = other.transform.position.x - this.transform.position.x;
            double checkX = (other.transform.lossyScale.x + this.transform.lossyScale.x) / 2;
            double diffZ = other.transform.position.z - this.transform.position.z;
            double checkZ = (other.transform.lossyScale.z + this.transform.lossyScale.z) / 2;
            if (diffX == checkX)
            {
                playerMovement.modifyPlayerAvailableMovements(player, (int)eCommands.RIGHT - 1, false);
            }
            if (diffX == -checkX)
            {     
                playerMovement.modifyPlayerAvailableMovements(player, (int)eCommands.LEFT - 1, false);
            }
            if (diffZ == checkZ)
            {
                playerMovement.modifyPlayerAvailableMovements(player, (int)eCommands.UP - 1, false);
            }
            if (diffZ == -checkZ)
            {
                playerMovement.modifyPlayerAvailableMovements(player, (int)eCommands.DOWN - 1, false);
            }  
        }
    }
}
