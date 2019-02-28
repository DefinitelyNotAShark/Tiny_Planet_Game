using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioClip shoot;

    [SerializeField]
    private float shootVolume;

    [Header("Bullet Settings")]
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletMuzzle;

    [SerializeField]
    private float bulletSpeed, bulletLifetime, shotDelay=0f, shotCooldown=0.5f;

    [Header("Shotgun Spread Settings")]
    [SerializeField]
    private int bulletCount;

    [SerializeField]
    private float spreadAngle;

    List<Quaternion> pellets;

    private GameObject bulletInstance;
    private Animator anim;
    private AudioSource audio;

    private bool coroutineStarted;

    private void Awake()
    {
        pellets = new List<Quaternion>(new Quaternion[bulletCount]);
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void AddBullet()//allows us to shoot another bullet in our gun at one time
    {
        Quaternion pellet = new Quaternion();
        pellets.Add(pellet);
    }

    void Update ()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            if (!coroutineStarted)
            {
                Shoot();
                StartCoroutine(ShootingCooldown());
                anim.SetBool("isShooting", true);
            }
        }
        else
            anim.SetBool("isShooting", false);
	}

    void Shoot()
    {
        audio.PlayOneShot(shoot, shootVolume);

        for(int i = 0; i < pellets.Count; i++)
        {
            pellets[i] = Random.rotation;

            bulletInstance = Instantiate(bulletPrefab, bulletMuzzle.position, bulletMuzzle.rotation);
            bulletInstance.transform.rotation = Quaternion.RotateTowards(bulletInstance.transform.rotation, pellets[i], spreadAngle);
            bulletInstance.AddComponent<MoveBullet>().speed = bulletSpeed;
            bulletInstance.GetComponent<MoveBullet>().lifetime = bulletLifetime;
        }
    }

    private IEnumerator ShootingCooldown()
    {
        coroutineStarted = true;
        //yield return new WaitForSeconds(shotDelay);
        
        //bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //bulletInstance.AddComponent<MoveBullet>().speed = bulletSpeed;
        //bulletInstance.GetComponent<MoveBullet>().lifetime = bulletLifetime;

        yield return new WaitForSeconds(shotCooldown);//tiny cooldown
        coroutineStarted = false;
    }
}
