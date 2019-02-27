using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [HideInInspector]
    public Transform playerTransform;

    [HideInInspector]
    public float minDistance;

    [HideInInspector]
    public float enemySpeed;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);//calculate distance from player

        if (distance > minDistance)//if the distance between us isn't the min distance
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);//move closer to player   
    }
}
