using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Tutorial for spawning random point
//https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float minTimeBetweenSpawn, maxTimeBetweenSpawn;

    [SerializeField]
    [Tooltip("The number of enemies spawned before the wave stops")]
    private int numberOfEnemiesInWave;

    [SerializeField]
    [Tooltip("The amount of time you get to catch your breath inbetween waves")]
    private float timeBetweenWaves;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float planetRadius;

    private GameObject objectInstance;

    void Start()
    {
        StartCoroutine(StartSpawning());//start the spawning loop
    }

    private IEnumerator StartSpawning()
    {

        for (; ; )//HACK maybe put this in a game loop later?
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            for (int i = 0; i < numberOfEnemiesInWave; i++)
            {
                yield return new WaitForSeconds(ChooseARandomSpawnTime());//waits a random time that it chooses from our min to our max
                Spawn();
            }
        }
    }

    private float ChooseARandomSpawnTime()
    {
        return Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    private void Spawn()
    {
        objectInstance = Instantiate(enemyPrefab, GenerateRandomPoint(), transform.localRotation);//spawns our enemy at the position of the spawn point and it's normal rotation 
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
        return new Vector3(x, y, z);
    }

    //public Vector3 RandomNavmeshLocation(float radius)
    //{
    //    NavMeshHit hit;
    //    Vector3 randomDirection = Random.insideUnitSphere * radius;
    //    Vector3 finalPosition = Vector3.zero;

    //    randomDirection += transform.position;

    //    if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
    //    {
    //        finalPosition = hit.position;
    //    }
    //    return finalPosition;
    //}

    ///// <summary>
    ///// Chooses a point at random from our array of transforms and returns the position
    ///// </summary>
    //private Vector3 ChooseARandomSpawnPoint()
    //{
    //    int randomPointIndex = Random.Range(0, spawnPoints.Length);
    //    Transform t;

    //    for (int i = 0; i < spawnPoints.Length; i++)
    //    {
    //        if (i == randomPointIndex)
    //        {
    //            t = spawnPoints[i];
    //            return t.position;
    //        }
    //    }
    //    t = spawnPoints[0];
    //    return t.position;//if it didn't choose a point, return the first one of the index
    //}
}
