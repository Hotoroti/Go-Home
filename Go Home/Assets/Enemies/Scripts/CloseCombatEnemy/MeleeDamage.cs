using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    private HealthBarUpdates baseHealth;

    private void Start()
    {
        baseHealth = GameObject.FindWithTag("HealthBar").GetComponent<HealthBarUpdates>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject == PlayerManager.instance.player && this.CompareTag("EnemyWeapon")) //If the other is the player continue
        {
            PlayerManager.instance.player.GetComponent<Player>().TakeDamage(Enemy.meleeEnemyDamage,0); //The player takes damage
            EnemyController.audioSource.Play();
        }

        if(other.CompareTag("Base"))
        {
            baseHealth.RemoveHP(Enemy.meleeEnemyDamage);
            EnemyController.audioSource.Play();
        }
    }
}
