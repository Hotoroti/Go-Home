using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{
    void Update()
    {
        Vector3 velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) velocity += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.A)) velocity += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.S)) velocity += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.D)) velocity += new Vector3(1, 0, 0);
        velocity.Normalize();
        velocity *= (Time.deltaTime * 10);

        transform.position += velocity;
    }
}
