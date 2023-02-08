using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//old code, do not use
public class TowerPlacementHandler : MonoBehaviour
{
    [SerializeField] GameObject buildingTower;
    [SerializeField] GameObject hologram;
    [SerializeField] int cooldown;
    private int towerInventory;
    private bool holochecker = false;
    private GameObject cloneHolo, newTower;   


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(location);
        if(cooldown > 0)
        {
            cooldown--;
        }
        

        if (cooldown <= 0 && towerInventory > 0 && Input.GetKey(KeyCode.E) && !holochecker)
        {
            cloneHolo = Instantiate(hologram);
            holochecker = true;
            cooldown = 100;
        }

        if (cooldown <= 0 && holochecker && Input.GetKey(KeyCode.E))
        {
            newTower = Instantiate(buildingTower, new Vector3(cloneHolo.transform.position.x, -22f, cloneHolo.transform.position.z), Quaternion.identity);
            Destroy(cloneHolo);
            towerInventory--;
            cooldown = 100;
            holochecker = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            if (Input.GetKey(KeyCode.E) && cooldown == 0)
            {
                Destroy(other.transform.gameObject);
                cooldown = 100;
                towerInventory++;
            }
        }
    }
}
