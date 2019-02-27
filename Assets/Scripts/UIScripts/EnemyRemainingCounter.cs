using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRemainingCounter : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner spawnScript;

    private int enemiesRemaining;
    private int enemiesSpawned;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        enemiesRemaining = spawnScript.enemiesOnScreen;
        enemiesSpawned = spawnScript.numberOfEnemiesInWave;

        if(enemiesRemaining < 0)
        {
            enemiesRemaining = 0;//don't want to show negative numbers that would make people confused;
        }

        text.text = enemiesRemaining + " / " + enemiesSpawned;
    }
}
