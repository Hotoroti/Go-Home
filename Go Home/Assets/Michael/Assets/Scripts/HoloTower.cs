using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloTower : MonoBehaviour
{
    private Transform holoLocation;

    // Update is called once per frame
    void Update()
    {
        //places the hologram prefab where the player is looking
        holoLocation = GameObject.Find("HoloLocation").transform;
        transform.position = holoLocation.transform.position;
    }
}
