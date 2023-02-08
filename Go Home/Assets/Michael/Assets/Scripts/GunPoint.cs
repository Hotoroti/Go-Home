using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire;
    [SerializeField] TowerTargeting towerTargeting;

    //get fire rate from the gun
    public float GetRateofFire()
    {
        return rateOfFire;
    }


    public void Fire()
    {
        Instantiate(projectile, transform.position, transform.rotation).GetComponent<Projectile>().SetTower(towerTargeting);
    }
}
