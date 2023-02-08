using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    protected float lookRadius = 10f; //the radius of the enemy

    protected Transform target; //The current target of the enemy
    Transform playerObj; //the player gameobject/transform
    Player player;
    static public AudioSource audioSource;
    Vector3 startSpeed;
    protected NavMeshAgent agent;

    [Header("CheckPoints")]
    private int routeSelector;
    [SerializeField]private int nRoutes;
    [SerializeField]private Transform[] checkPointsRoute1;
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

    public Animations enemyAnimation;
    private bool choosePath = true;

    private int routeSpawner1, routeSpawner2;
    private bool route1Spawner1, route1Spawner2;

    private float defaultStoppingDistance;
    private float baseStoppingDistance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerObj = PlayerManager.instance.player.transform;
        player = playerObj.GetComponent<Player>();
        enemyAnimation = GetComponentInChildren<Animations>();
        audioSource = GetComponent<AudioSource>();

        RouteStart();

        startSpeed = agent.velocity;

        defaultStoppingDistance = agent.stoppingDistance;
        baseStoppingDistance = defaultStoppingDistance + 1.5f;

    }

    public void Update()
    {
        WhichTarget();
        if (target == null) return;        
        //Move to the target
        agent.SetDestination(target.position);

        Attack();
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
        float distance = Vector3.Distance(target.position, transform.position); //the distance between the target and the enemy
        if (distance <= agent.stoppingDistance) //When the distance is smaller than the stopping distance
        {
            enemyAnimation.enemieCurrentState = "Melee"; //Switch the state of the animation
            FaceTarget();
        }
        else
        {
            enemyAnimation.enemieCurrentState = "Walking"; //changes the state of the animation
        }
    }
    #region Target
    protected void FaceTarget()
    {
        //Faces the target
        Vector3 direction = (target.position - transform.position).normalized; //What direction needs the enemy look
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); //Creates the rotation on all the axis
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); //Smoothly turns to the good direction        
    }
    void WhichTarget()
    {
        //Selects the route the enemy takes
        if (route1Spawner1 && !route1Spawner2) TargetRoute1();
        if (!route1Spawner1 && !route1Spawner2) TargetRoute3();
        if (!route1Spawner1 && route1Spawner2) TargetRoute2();

        PlayerTarget();

    }
    void PlayerTarget()
    {
        //The distance for the player
        var distancePlayer = Vector3.Distance(playerObj.position, transform.position);
        if (distancePlayer <= lookRadius)
        {
            target = playerObj;
        }        
    }

    void TargetRoute1()
    {
        //If there are no checkpoints the code still runs without errors
        if (checkPointsRoute1 == null)
        {
            target = playerObj;
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
    private void OnDrawGizmosSelected() //Draw the guidelines
    {
        Gizmos.color = Color.red; //The color of the circle is red
        Gizmos.DrawWireSphere(transform.position, lookRadius); //Draw guidelines as a sphere on the position with the size of the lookradius 
    }
}
