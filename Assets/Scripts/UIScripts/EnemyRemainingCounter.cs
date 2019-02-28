using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRemainingCounter : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    private int enemiesRemaining;
    private int enemiesSpawned;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        enemiesRemaining = manager.EnemiesOnScreen;
        enemiesSpawned = manager.numberOfEnemiesInWave;

        if(enemiesRemaining < 0)
        {
            enemiesRemaining = 0;//don't want to show negative numbers that would make people confused;
        }

        text.text = enemiesRemaining + " / " + enemiesSpawned;
    }
}
