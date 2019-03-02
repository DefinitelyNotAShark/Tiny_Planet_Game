using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float enemySpeed, maxEnemySpeed;

    [SerializeField]
    [Tooltip("The minimum distance the enemy can spawn to the player")]
    private float minDistanceMoveTowardsPlayer;

    [SerializeField]
    private GameObject implosionPrefab;


    private GameObject objectInstance;
    private PlayerShoot shootScript;//allows me to change the gun type in this script

    /// <summary>
    /// Spawn a new enemy at the passed in position
    /// </summary>
    /// <param name="spawnPoint"></param>
    public void Spawn(Vector3 spawnPoint)
    {
        //instantiate the enemy
        objectInstance = Instantiate(enemyPrefab, spawnPoint, transform.localRotation);//spawns our enemy at the position of the spawn point and it's normal rotation 

        //set the variables to the serialize field ones
        objectInstance.AddComponent<EnemyMove>().playerTransform = playerTransform;
        objectInstance.GetComponent<EnemyMove>().minDistance = minDistanceMoveTowardsPlayer;
        objectInstance.GetComponent<EnemyMove>().enemySpeed = enemySpeed;
        objectInstance.GetComponent<EnemyDetectColl>().implosion = implosionPrefab;
    }

    public void IncreaseSpeed()
    {
        if(enemySpeed < maxEnemySpeed)
        {
            enemySpeed++;
        }
    }
}
