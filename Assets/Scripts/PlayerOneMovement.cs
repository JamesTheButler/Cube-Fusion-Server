using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMovement : MonoBehaviour
{

    float speed = 3.0f;
    Vector3 pos;
    Transform tr;

    void Start()
    {
        pos = transform.position;
        tr = transform;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.D) && tr.position == pos)
        {
            pos += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A) && tr.position == pos)
        {
            pos += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.W) && tr.position == pos)
        {
            pos += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S) && tr.position == pos)
        {
            pos += Vector3.back;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
}
