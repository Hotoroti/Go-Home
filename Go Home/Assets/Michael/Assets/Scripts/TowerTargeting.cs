using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    private GameObject resetPoint;
    public Vector3 _targetPoint;
    private GunPoint currentGun;
    private GunRotation currentGunRotation;
    private float fireRate, fireRateDelta;

    // Start is called before the first frame update
    void Start()
    {
        //get info from gun attached to tower 
        currentGun = GetComponentInChildren<GunPoint>();
        currentGunRotation = GetComponentInChildren<GunRotation>();
        fireRate = currentGun.GetRateofFire();
        fireRateDelta = fireRate;
        resetPoint = GameObject.FindGameObjectWithTag("ResetPoint");
    }

    public Vector3 targetPosition()
    {
        return _targetPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //if there is any enemy in the list, start firing
        if (enemyList.Count > 0)
        {
            fireRateDelta -= Time.deltaTime;
            if (fireRateDelta <= 0)
            {
                currentGun.Fire();
                fireRateDelta = fireRate;
            }
            _targetPoint = enemyList[0].transform.position;

        }

        //no enemy in list, look at the reset point instead. be inactive
        if (enemyList.Count == 0)
        {
            fireRateDelta = fireRate;
            _targetPoint = resetPoint.transform.position;            
        }
    }

    //if an enemy enters the tower's range, add it to the list. if it exits range, remove from list
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy")
        {            
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }
}
