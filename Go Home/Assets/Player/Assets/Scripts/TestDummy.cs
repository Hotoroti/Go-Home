using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    private GameObject playerObject;
    private Player player;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(10, 0);
        }
    }
}