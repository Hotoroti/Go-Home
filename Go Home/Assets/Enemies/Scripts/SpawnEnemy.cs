using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy; //All enemies that can spawn
    [SerializeField] private float spawnRadius; //The Radius the enemies can spawn
    [SerializeField] List<GameObject> spawnerList; //List of the spawners for a level

    private float waveTime; //The Time in a wave
    private float timeForNextWave; //The time counter for a new wave
    [SerializeField] private float maxTimeInWave; //The time the enemies can spawn in a wave
    [SerializeField] private float nextWaveTime; //The time for the next wave
    
    private int iSpawner; //the currentspawner
    private Vector3 spawnerPos; //The position of the spawner
    public static int route;

    private float spawnTime; //The time counter to spawn a new enemy
    [SerializeField] private float newEnemySpawn; //The time for a new enemy to spawn

    [SerializeField] private int nEnemy; //The number of enemies that spawn in the spawntime

    public static int whatSpawner; //An int for in another script

    private void Update()
    {
        spawnTime += Time.deltaTime;

        StartCoroutine(EnemySpawn()); //Start to spawn enemies
        if (waveTime < maxTimeInWave) return;
        timeForNextWave += Time.deltaTime;

        if (timeForNextWave < nextWaveTime) return;
        waveTime = 0;
        timeForNextWave = 0;
        
    }

    IEnumerator EnemySpawn()
    {
        while (WaveTimer.waveTimer > 0) //if the waveTimer is more than 0 it can spawn enemies
        {
            if (spawnTime >= newEnemySpawn) //The time in a wave to spawn enemies
            {
                iSpawner = Random.Range(0, spawnerList.Count); //What spawner is the enemy going to spawn

                if (iSpawner == 0)
                {
                    spawnerPos = spawnerList[iSpawner].transform.position; //The spawnposition is the position of the spawner it choose
                    whatSpawner = iSpawner; //what spawner is going to be needed for enemy route so the value is the same as iSpawner
                }

                if (spawnerList[1] != null) //if the second spawner is not empty
                {
                    if (iSpawner == 1)
                    {
                        spawnerPos = spawnerList[iSpawner].transform.position;  //The spawnposition is the position of the spawner it choose
                        whatSpawner = iSpawner; //what spawner is going to be needed for enemy route so the value is the same as iSpawner
                    }
                }

                for (int i = 0; i < nEnemy; i++) //How many enemies spawn
                {
                    var spawnPos = spawnerPos; //The position the enemy spawn
                    var randomInCircle = Random.insideUnitSphere.normalized * spawnRadius; //The position is some random plae in a sphere
                    spawnPos = new Vector3(spawnPos.x + randomInCircle.x, spawnPos.y, spawnPos.z + randomInCircle.z);//the position changes on the x and z axis
                    Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPos, Quaternion.identity); //Instatiate the enemies
                }
                spawnTime = 0; //no new enemies spawn
            }
            yield return null;   //needs to return something so we do zero         
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
