using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashExplosion : MonoBehaviour
{
    private ParticleSystem dashParticleSystem;
    private Collider dashCollider;
    private PlayerAudioManager playerAudioManager;

    private int dashFireDamage;
    private int dashExplisionDamage;
    private float explosionTimer;
    private float particleDuration;
    private bool explosionAudio;

    void Start()
    {
        dashParticleSystem = GetComponent<ParticleSystem>();
        dashCollider = GetComponent<Collider>();
        playerAudioManager = GameObject.Find("PlayerAudioManager").GetComponent<PlayerAudioManager>();
        if (gameObject.name == "Explosion")
            explosionTimer = dashParticleSystem.emission.GetBurst(0).time;
        
        particleDuration = dashParticleSystem.main.duration;
        dashFireDamage = 5;
        dashExplisionDamage = 10;
        explosionAudio = false;

        DestroyParentGameObject();
    }

    private void Update()
    {
        // Plays explosion after a delay
        if (gameObject.name == "Explosion")
        {
            explosionTimer -= Time.deltaTime;
            if (explosionTimer <= 0)
            {
                dashCollider.enabled = true;
                if (!explosionAudio)
                    StartCoroutine(RageDashExplosionAudio()); 
            }
        }
    }

    // Collision between DashFire/DashExplosion & enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (gameObject.name == "FireOrbs")
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(dashFireDamage, 0);
            }

            if (gameObject.name == "Explosion")
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(dashExplisionDamage, 0);
            }
        }
    }

    // Player explosion audio only once 
    private IEnumerator RageDashExplosionAudio()
    {
        explosionAudio = true;
        yield return new WaitForFixedUpdate();
        playerAudioManager.Play("RageDashExplosion");
    }

    // Destroys parent after the explosion is done
    private void DestroyParentGameObject()
    {
        if (gameObject.transform.parent.gameObject != null)
            Destroy(gameObject.transform.parent.gameObject, particleDuration);
    }
}
