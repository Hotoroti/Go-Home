using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.AI;

public class Range_Attack : MonoBehaviour
{
    protected float lookRadius = 10f; //The radius the enemy can look at

    protected Transform target; //The target
    Transform player; //The player gameobject/transform
    protected NavMeshAgent agent;
    Vector3 startVelocity;
    private bool isInRange = false; //A boolean to look at if the player is in range or not

    public static Vector3 targetPos;
    [SerializeField] GameObject projectile; //The projectile gameobject what the enemy is shooting

    private bool choosePath = true;

    private float defaultStoppingDistance;
    private float baseStoppingDistance;

    AudioSource audio;

    #region CheckPoints
    [Header("CheckPoints")]
    private int routeSelector;
    [SerializeField] private int nRoutes;
    [SerializeField] private Transform[] checkPointsRoute1;
    private int pointsIndexRoute1;
    [SerializeField] private Transform[] checkPointsRoute2;
    private int pointsIndexRoute2;
    [SerializeField] private Transform[] checkPointsRoute3;
    private int pointsIndexRoute3;
    [SerializeField] private Transform[] checkPointsRoute4;
    private int pointsIndexRoute4;
    [SerializeField] private Transform[] checkPointsRoute5;
    private int pointsIndexRoute5;
    [SerializeField] private Transform[] checkPointsRoute6;
    private int pointsIndexRoute6;
    [SerializeField] private Transform[] checkPointsRoute7;
    private int pointsIndexRoute7;
    [SerializeField] private Transform[] checkPointsRoute8;
    private int pointsIndexRoute8;
    [SerializeField] private Transform[] checkPointsRoute9;
    private int pointsIndexRoute9;
    [SerializeField] private Transform[] checkPointsRoute10;
    private int pointsIndexRoute10;
    #endregion

    public Animations enemyAnimations; //The animations the enemy uses of the animations class

    public static bool startSpawningBullet; //Bool to spawn a bullet. Connects to the animations events

    private int routeSpawner1, routeSpawner2;
    private bool route1Spawner1, route1Spawner2;
    Rigidbody rb;

    [SerializeField] private GameObject weapon; //The weapon of the enemy
    private void Start()
    {
        RouteStart();
        agent = GetComponent<NavMeshAgent>();
        player = PlayerManager.instance.player.transform; //Instantiate the player
        rb = GetComponent<Rigidbody>();

        audio = GetComponent<AudioSource>();
        startVelocity = agent.velocity;

        defaultStoppingDistance = agent.stoppingDistance;
        baseStoppingDistance = defaultStoppingDistance + 1.5f;

    }

    public void Update()
    {
        WhichTarget();
        if (target == null) return; //If there is no target the code does not run
        agent.SetDestination(target.position); //The enemy goes to the target
        Attack();
        if (target == gameObject.CompareTag("Base"))
        {
            agent.stoppingDistance = baseStoppingDistance;
        }
        else
        {
            agent.stoppingDistance = defaultStoppingDistance;
        }

    }

    void RouteStart()
    {
        if (SpawnEnemy.whatSpawner == 0)
        {
            var route1 = true;
            if (checkPointsRoute3 != null)
            {
                var randomRoute = Random.Range(0, 2);
                if (randomRoute == 0)
                {
                    route1 = true;
                }
                else if (randomRoute == 1)
                {
                    route1 = false;
                }
            }

            if (checkPointsRoute1 != null && route1)
            {
                route1Spawner1 = true;
                route1Spawner2 = false;
            }
            else if (checkPointsRoute3 != null && !route1)
            {
                route1Spawner1 = false;
                route1Spawner2 = false;
            }
        }

        if (SpawnEnemy.whatSpawner == 1)
        {
            route1Spawner1 = false;
            route1Spawner2 = true;
        }

    }
    public virtual void Attack()
    {
        float distance = Vector3.Distance(target.position, transform.position); //The distance between the target and the enemy
        if (distance <= agent.stoppingDistance) //if the target is in range continue
        {
            enemyAnimations.enemieCurrentState = "Shooting"; //The aniation state is shooting
            isInRange = true;
            targetPos = target.transform.position;
            StartCoroutine(spawnProjectile()); //Start to spawn an projectile
            
            FaceTarget(); //faces the target
        }
        else if(distance > agent.stoppingDistance) //if the distance is larger than the stoppingdistance continue
        {
            enemyAnimations.enemieCurrentState = "Walking"; //the animation state is walking
            isInRange = false; //the target is not in range
        }        
    }
    #region Targets
    protected void FaceTarget()
    {
        //Faces the target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void WhichTarget()
    {
        //Selects the route the enemy takes
        if (route1Spawner1 && !route1Spawner2) TargetRoute1();
        else if (!route1Spawner1 && !route1Spawner2) TargetRoute3();
        else if (!route1Spawner1 && route1Spawner2) TargetRoute2();

        PlayerTarget();

    }
    void PlayerTarget()
    {
        //The distance for the player
        var distancePlayer = Vector3.Distance(player.position, transform.position);
        if (distancePlayer <= lookRadius)
        {
            target = player;
        }
    }

    void TargetRoute1()
    {
        //If there are no checkpoints the code still runs without errors
        if (checkPointsRoute1 == null)
        {
            target = player;
            return;
        }

        //Checks if the checkpoint index is smaller than the length of the array
        if (pointsIndexRoute1 <= checkPointsRoute1.Length - 1)
        {
            //Changes the target
            target = checkPointsRoute1[pointsIndexRoute1];
            //Checks the distance between the checkpoint and the enemy
            var distanceCheckpointsRoute1 = Vector3.Distance(checkPointsRoute1[pointsIndexRoute1].position, transform.position);
            //If the checkpoint is in the range of the enemy
            //It goes to the next checkpoint
            if (distanceCheckpointsRoute1 <= lookRadius)
            {
                pointsIndexRoute1++;
            }

        }
    }

    void TargetRoute2()
    {
        if (checkPointsRoute2.Length == 0)
        {
            routeSelector--;
            return;
        }
        if (pointsIndexRoute2 <= checkPointsRoute2.Length - 1)
        {
            target = checkPointsRoute2[pointsIndexRoute2];
            var distanceCheckpointsRoute = Vector3.Distance(checkPointsRoute2[pointsIndexRoute2].position, transform.position);
            if (distanceCheckpointsRoute <= lookRadius)
            {
                pointsIndexRoute2++;
            }

        }
    }
    void TargetRoute3()
    {
        if (checkPointsRoute3.Length == 0)
        {
            routeSelector--;
            return;
        }
        if (pointsIndexRoute3 <= checkPointsRoute3.Length - 1)
        {
            target = checkPointsRoute3[pointsIndexRoute3];
            var distanceCheckpointsRoute = Vector3.Distance(checkPointsRoute3[pointsIndexRoute3].position, transform.position);
            if (distanceCheckpointsRoute <= lookRadius)
            {
                pointsIndexRoute3++;
            }

        }
    }   
    #endregion

    IEnumerator spawnProjectile()
    {
        while (startSpawningBullet == true && isInRange) //when to spawn the bullet
        {
            audio.Play();
            Instantiate(projectile, weapon.transform.position, Quaternion.identity); //instantiate the bullet
            startSpawningBullet = false; //the startSpawningBullet is false
            yield return null; //it needs to return something so we return null
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
