using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollowPlayer : MonoBehaviour
{
    private Transform player;
    private Transform minimapCamera;

    void Start()
    {
        //Get the player and the minimap camera
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        minimapCamera = GameObject.FindGameObjectWithTag("MinimapCam").GetComponent<Transform>();
    }

    void Update()
    {
        //Set the position of the camera above the player
        minimapCamera.position = new Vector3(player.position.x, player.position.y+10, player.position.z);
    }
}
