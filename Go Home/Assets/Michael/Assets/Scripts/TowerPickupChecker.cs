using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//old code. do not use
public class TowerPickupChecker : MonoBehaviour
{

    public int _towerInventory;
    public int _cooldown = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //can we have multiple trigger colliders under different child objects? does that work?\
    //otherwise place this under tower script, destroy gameobject
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.transform.gameObject);
                _towerInventory++;
            }
        }
    }
}
