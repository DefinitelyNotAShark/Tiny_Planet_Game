using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Tutorial for spawning random point
//https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]
    public int enemiesOnScreen;

    [Tooltip("The number of enemies spawned before the wave stops")]
    public int numberOfEnemiesInWave;

    [SerializeField]
    private float minTimeBetweenSpawn, maxTimeBetweenSpawn;

    [SerializeField]
    [Tooltip("The amount of time you get to catch your breath inbetween waves")]
    private float timeBetweenWaves;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float planetRadius;

    [SerializeField]
    private float enemySpeed;

    [SerializeField]
    [Tooltip("The minimum distance the enemy can spawn to the player")]
    private float minDistanceSpawnFromPlayer, minDistanceMoveTowardsPlayer;

    [SerializeField]
    private WaveAlert waveUI;

    [SerializeField]
    private SpawnHealth healthSpawnScript;

    private GameObject objectInstance;

    void Start()
    {
        enemiesOnScreen = 0;
        StartCoroutine(StartSpawning());//start the spawning loop
    }

    private IEnumerator StartSpawning()
    {
        for (; ; )//HACK maybe put this in a game loop later?
        {
            if (enemiesOnScreen == 0)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
                StartCoroutine(waveUI.StartAlert());

                for (int i = 0; i < numberOfEnemiesInWave; i++)
                {
                    yield return new WaitForSeconds(ChooseARandomSpawnTime());//waits a random time that it chooses from our min to our max
                    Spawn();
                    enemiesOnScreen++;
                }

                healthSpawnScript.SpawnHealthPacks();//put some health on the screen after enemies have spawned
            }

            yield return new WaitForEndOfFrame();//lil pause
            Debug.Log("Enemies Left: " + enemiesOnScreen.ToString());
        }
    }

    private float ChooseARandomSpawnTime()
    {
        return Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    private void Spawn()
    {
        objectInstance = Instantiate(enemyPrefab, GenerateRandomPoint(), transform.localRotation);//spawns our enemy at the position of the spawn point and it's normal rotation 
        objectInstance.AddComponent<EnemyMove>().playerTransform = playerTransform;
        objectInstance.GetComponent<EnemyMove>().minDistance = minDistanceMoveTowardsPlayer;
        objectInstance.GetComponent<EnemyMove>().enemySpeed = enemySpeed;
    }

    private Vector3 GenerateRandomPoint()
    {
        float x, y, z, d;
        do
        {
            x = Random.Range(-planetRadius, planetRadius);
            y = Random.Range(-planetRadius, planetRadius);
            z = Random.Range(-planetRadius, planetRadius);
            d = x * x + y * y + z * z;
        } while (d > 1.0);

        float distance = Vector3.Distance(new Vector3(x, y, z), playerTransform.position);//calculate distance between player and possible enemy spawn point

        if (distance < minDistanceSpawnFromPlayer)//if the enemy is too close to the player, 
        {
            Debug.Log("the player was too close. Distance was " + distance.ToString());
            GenerateRandomPoint();//try coming up with a new point
        }

        return new Vector3(x, y, z);
    }
}
