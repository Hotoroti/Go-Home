using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] GameObject explosion;
    Vector3 targetLocation;
    private TowerTargeting tower;

    // Start is called before the first frame update
    void Start()
    {
        targetLocation = tower.targetPosition();        
    }

    //make sure correct tower is affected
    internal void SetTower(TowerTargeting tt)
    {
        tower = tt;
    }

    // Update is called once per frame
    void Update()
    {
        //if there is an enemy in the list, fire
        if (tower.enemyList != null)
        {
            transform.Translate(new Vector3(0f, 0f, projectileSpeed * Time.deltaTime));
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //check collisions and instantiate explosions
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            tower.enemyList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            Instantiate(explosion, transform.position, transform.rotation).GetComponent<Explosion>().SetTower(tower);
            Destroy(gameObject);
        }
    }
}
