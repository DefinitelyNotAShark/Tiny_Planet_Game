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

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        animation = GetComponentInParent<Animation>();
    }

    void Update ()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            StartCoroutine(ShootWithAnimation());
            anim.SetBool("isShooting", true);
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
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.AddComponent<MoveBullet>().speed = bulletSpeed;
        bulletInstance.GetComponent<MoveBullet>().lifetime = bulletLifetime;
    }
}
