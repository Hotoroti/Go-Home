using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    public GameObject buildingTower;
    public GameObject Hologram;
    private GameObject newBuild, newHologram;
    [SerializeField] private int towerInventory;
    [SerializeField] private UiTowerIconManager towers;
    private float cooldown;
    private float buildingSpawn = -22f;
    private bool hologramActive, towerRange, destroyTower;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }

        //if there is no hologram being placed, no towers are in range and the player has a tower in inventory, start building by placing
        //the hologram
        if (Input.GetKeyDown(KeyCode.E) && !hologramActive && towerInventory > 0 && cooldown <= 0 && !towerRange)
        {
            newHologram = Instantiate(Hologram);
            hologramActive = true;
            cooldown = 100;

        }

        //if there is a tower in range of the player and there is no hologram active, pick up the tower instead
        else if (Input.GetKeyDown(KeyCode.E) && !hologramActive && cooldown <= 0 && towerRange)
        {
            towerInventory++;
            cooldown = 100;
            destroyTower = true;
        }

        //if a hologram is active, continue building by starting the process and removing tower from inventory
        else if (Input.GetKeyDown(KeyCode.E) && hologramActive && cooldown <= 0)
        {
            newBuild = Instantiate(buildingTower, new Vector3(newHologram.transform.position.x, buildingSpawn, newHologram.transform.position.z), Quaternion.identity);
            Destroy(newHologram);
            hologramActive = false;
            towerInventory--;
            towers.Deactivate();
            cooldown = 100;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //check if towers are in range and adjust booleans
        if (other.gameObject.tag == "Tower")
        {
            towerRange = true;

            if (destroyTower)
            {
                Destroy(other.gameObject);
                towerRange = false;
                destroyTower = false;
            }

        }
    }
}
