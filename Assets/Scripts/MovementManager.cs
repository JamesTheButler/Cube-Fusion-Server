using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    float speed = 3.0f;
    Vector3 pos;
    Vector3 targetPos;


    public void mooveForward(float speed)
    {
        if (isOnASquare())
        {
            pos = transform.position + Vector3.forward;
        }
    }
    public void mooveRight()
    {
        mooveRight(this.speed);
    }
    public void mooveRight(float speed)
    {
        if (isOnASquare())
        {
            pos = transform.position + Vector3.right;
        }
    }
    public void mooveLeft()
    {
        mooveLeft(this.speed);
    }
    public void mooveLeft(float speed)
    {
        if (isOnASquare())
        {
            pos = transform.position + Vector3.left;
        }
    }
    public void mooveBack()
    {
        mooveBack(this.speed);
    }
    public void mooveBack(float speed)
    {
        if (isOnASquare())
        {
            pos = transform.position + Vector3.back;
        }
    }
    public void mooveForward()
    {
        mooveForward(this.speed);
    }



    public void moove()
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
    //Checks that the square reached its location
    bool isOnASquare()
    {
        Vector3 currentPosition = transform.position;
        return (currentPosition.x % 1 == 0 && currentPosition.z % 1 == 0) ? true : false;

    }
}
