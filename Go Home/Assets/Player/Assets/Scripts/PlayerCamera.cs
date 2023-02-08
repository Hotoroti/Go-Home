using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject playerObject;

    private float speed;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;
    private Quaternion rotation;

    private void Start()
    {
        speed = 20;
        offset = new Vector3(0, 30, -30);
        rotation = new Quaternion(0.382683456f, 0, 0, 0.923879504f);

        playerObject = GameObject.Find("Player");
        transform.SetPositionAndRotation(transform.position, rotation);
    }

    // Makes the camera follow the player
    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, playerObject.transform.position + offset, ref velocity, speed * Time.deltaTime);
    }
}
