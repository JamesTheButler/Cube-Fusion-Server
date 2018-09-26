using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMovement : MonoBehaviour {

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

        if (Input.GetKey(KeyCode.RightArrow) && tr.position == pos)
        {
            pos += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && tr.position == pos)
        {
            pos += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && tr.position == pos)
        {
            pos += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && tr.position == pos)
        {
            pos += Vector3.back;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
}
