using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float timer;
    private TowerTargeting tower;


    //ensure correct tower list is edited
    internal void SetTower(TowerTargeting tt)
    {
        tower = tt;
    }

    private void Update()
    {
        //make explosion disappear within x amount of time
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    // kills enemies if hit by explosion
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            tower.enemyList.Remove(other.gameObject);
            Destroy(other.gameObject);
           
        }
    }
}
