using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial for spawning random point
//https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public int EnemiesOnScreen { get; private set; }

    [Header("Values")]
    [Tooltip("The number of enemies spawned before the wave stops")]
    public int numberOfEnemiesInWave;

    [SerializeField]
    [Tooltip("The amount of time you get to catch your breath inbetween waves")]
    private float timeBetweenWaves;

    [SerializeField]
    private float planetRadius;

    [Header("Scripts")]
    [SerializeField]
    private WaveAlert waveUI;

    [SerializeField]
    private GunUpgradedAlert gunUI;

    [SerializeField]
    private PlayerShoot shootScript;//allows me to change the gun type in this script

    private SpawnHealth healthSpawnScript;
    private EnemySpawner enemySpawnScript;

    private bool isStart = true;
    private int waveCount = 1;//waves start at 1
    private AudioSource audio;

    void Start ()
    {
        healthSpawnScript = GetComponentInChildren<SpawnHealth>();
        enemySpawnScript = GetComponentInChildren<EnemySpawner>();
        audio = GetComponentInChildren<AudioSource>();//get the audiosource on the enemyspawner script
        
        EnemiesOnScreen = 0;//starts off with no enemies on the screen
        StartCoroutine(StartGame());//start the spawning loop
    }

    private IEnumerator StartGame()
    {
        for (; ; )//game loop
        {
            if (EnemiesOnScreen == 0)
            {
                //STUFF THAT INCREASES WITH EACH WAVE
                if (!isStart)//only do this stuff on the next waves, not on the first one
                {
                    //can't give a million bullets. Maxes out at 10
                    if (waveCount < 10)
                    {
                        shootScript.AddBullet();//add one bullet to the gun to shoot at a time. start with 0 and get one right as you start
                        StartCoroutine(gunUI.StartAlert());//tell the player that their gun has been upgraded
                    }

                    //this is the stuff that doesn't max out at 10
                    enemySpawnScript.IncreaseSpeed();//make em faster
                    numberOfEnemiesInWave++;//add another enemy to the next wave each time
                    healthSpawnScript.SpawnHealthPacks(GenerateRandomPoint());//put some health on the screen after enemies have spawned
                }

                isStart = false;

                //ALERT FOR NEW WAVE
                yield return new WaitForSeconds(timeBetweenWaves);//the calm before the next wave
                StartCoroutine(waveUI.StartAlert());//give the alert that there will be a new wave
                //audio.PlayOneShot(happyAlert, happyAlertVolume);//play after a short delay so that you see it as soon as the UI is done fading in

                //NEXT WAVE
                waveCount++;
                for (int i = 0; i < numberOfEnemiesInWave; i++)
                {
                    //yield return new WaitForSeconds(ChooseARandomSpawnTime());//waits a random time that it chooses from our min to our max
                    enemySpawnScript.Spawn(GenerateRandomPoint());
                    EnemiesOnScreen++;
                }
            }

            yield return new WaitForEndOfFrame();//lil pause
        }
    }


    /// <summary>
    /// Generates a random Vector3 inside of the range of the sphere. Gets pushed out to the surface by the gravity scripts
    /// </summary>
    /// <returns></returns>
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

    public void DecreaseEnemyAmount()
    {
        EnemiesOnScreen--;
        audio.Play() ;//play enemy death sound
    }
}
