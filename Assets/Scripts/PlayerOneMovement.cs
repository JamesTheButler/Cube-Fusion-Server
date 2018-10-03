using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMovement : MonoBehaviour{
    float speed = 3.0f;
    Vector3 pos;
    enum Direction {Left, Right, Up, Down, Wait};

    void Update()
    {
        bool canBeMoved = isOnASquare();
        if (Input.GetKey(KeyCode.D) && canBeMoved)
        {
            pos += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A) && canBeMoved)
        {
            pos += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.W) && canBeMoved)
        {
            pos += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S) && canBeMoved)
        {
            pos += Vector3.back;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void movePlayer(List<int> movements)
    {

    }


    //Checks that the cube reached its location
    bool isOnASquare()
    {
        Vector3 currentPosition = transform.position;
        bool check = (currentPosition.x % 1 == 0 && currentPosition.z % 1 == 0) ? true : false;
        return check;        
    }

    public void setPos(Vector3 newPos) {
        pos = newPos;
        transform.position = newPos;
    }
}
