using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float enemyHealth; //The health of the enemy
    [SerializeField]
    private float damage; //The damage some enemys does

    Vector3 direction;

    static public float meleeEnemyDamage;
    [SerializeField]
    private float speed; //The speed of the enemy

    private bool knockBack;

    NavMeshAgent agent;
    Rigidbody rb;
    private float knockBackTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        meleeEnemyDamage = damage;
        knockBackTime = 1;
        knockBack = false;
    }
    private void Update()
    {
        //if(!knockBack) direction = gameObject.transform.forward;
        Die();
        agent.speed = speed;
    }
    private void Die()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage, float knockbackforce) //the take damage function of the enemy
    {
        enemyHealth -= damage;
        //StartCoroutine(KnockBack(knockbackforce));
    }

    private IEnumerator KnockBack(float knockbackForce)
    {
        knockBack = true;

        float startTime = Time.time;
        Vector3 direction = -this.direction;

        while (Time.time < startTime + knockBackTime)
        {
            rb.MovePosition(transform.position + (direction * knockbackForce * Time.deltaTime));

            yield return null;
        }

        knockBack = false;

    }

}
