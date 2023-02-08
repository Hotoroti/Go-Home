using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float speed; //The Speed of the bullet
    [SerializeField] float lifeSpan; //How long the bullet lifes
    Rigidbody rig;
    Transform playerObj;
    Player player;
    Vector3 direction; //The direction the bullet needs to go

    private HealthBarUpdates baseHealth;
    [SerializeField] private float damage; //The damage the bullet does
    private void Start()
    {
        baseHealth = GameObject.FindWithTag("HealthBar").GetComponent<HealthBarUpdates>();
        rig = GetComponent<Rigidbody>();
        playerObj = PlayerManager.instance.player.transform;
        player = playerObj.GetComponent<Player>(); //getting the player class by use getcomponent
        Physics.IgnoreLayerCollision(9, 10, true); //Ignore the collisions between layer 9 and 10. So the bullets do not hit the enemy
        Physics.IgnoreLayerCollision(10, 10, true); //Ignore the collisions between layer 10 and 10. So the bullets do not hit the bullets
        direction = Range_Attack.targetPos - transform.position; //Calculates the direction to the player
        rig.AddForce(direction * speed, ForceMode.Impulse); //the bullet is moving to the direction of the player
        Destroy(gameObject, lifeSpan); //Destroys the bullet after the lifespan is done
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //If the other collider is player
        {
            player.TakeDamage(damage, 0); //Player takes damage
            Destroy(gameObject); //Destroyes the bullet
        }

        if (other.CompareTag("Base"))
        {
            baseHealth.RemoveHP(damage);
            Destroy(gameObject);
        }
    }
}
