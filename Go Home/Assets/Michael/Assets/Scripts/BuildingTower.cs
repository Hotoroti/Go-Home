using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTower : MonoBehaviour
{
    [SerializeField] float moveUp;
    [SerializeField] GameObject tower;
    private GameObject player;
    private float posX, posY, posZ, offset;
    private GameObject newTower;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        posY = transform.position.y;
        posZ = transform.position.z;   
        posX = transform.position.x;
        offset = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //make tower appear from ground
        posY += moveUp * Time.deltaTime;
        transform.position = new Vector3(posX,posY,posZ);

        //if the building tower is at same height relatively to player, build real tower
        if (posY >= player.transform.position.y-offset)
        {
            newTower = Instantiate(tower, new Vector3(transform.position.x, player.transform.position.y-offset, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
