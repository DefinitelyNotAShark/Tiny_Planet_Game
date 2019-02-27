using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
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

    private bool coroutineStarted;

    private void Awake()
    {
        pellets = new List<Quaternion>(new Quaternion[bulletCount]);
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
        //AUDIO shooting sound
        int i = 0;
        foreach (Quaternion quat in pellets)
        {
            pellets[i] = Random.rotation;
            bulletInstance = Instantiate(bulletPrefab, bulletMuzzle.position, bulletMuzzle.rotation);
            bulletInstance.transform.rotation = Quaternion.RotateTowards(bulletInstance.transform.rotation, pellets[i], spreadAngle);
            bulletInstance.AddComponent<MoveBullet>().speed = bulletSpeed;
            bulletInstance.GetComponent<MoveBullet>().lifetime = bulletLifetime;
            i++;
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
