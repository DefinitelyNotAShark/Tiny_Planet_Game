using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed, bulletLifetime;

    private GameObject bulletInstance;
    private Animator anim;

    private bool coroutineStarted;
    
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update ()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            if (!coroutineStarted)
            {
                StartCoroutine(ShootWithAnimation());
                anim.SetBool("isShooting", true);
            }
        }
        else
            anim.SetBool("isShooting", false);
	}

    void Shoot()
    {
        //AUDIO shooting sound
        bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.AddComponent<MoveBullet>().speed = bulletSpeed;
        bulletInstance.GetComponent<MoveBullet>().lifetime = bulletLifetime;
    }

    private IEnumerator ShootWithAnimation()
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(.2f);

        bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.AddComponent<MoveBullet>().speed = bulletSpeed;
        bulletInstance.GetComponent<MoveBullet>().lifetime = bulletLifetime;

        yield return new WaitForSeconds(.5f);//tiny cooldown
        coroutineStarted = false;
    }
}
