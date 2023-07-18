using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCam : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
        }
    }
}
