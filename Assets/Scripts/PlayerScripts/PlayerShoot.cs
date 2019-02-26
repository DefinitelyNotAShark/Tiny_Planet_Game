﻿using System.Collections;
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
    }

    void Update ()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            Shoot();
            anim.SetTrigger("Shoot");
        }
	}

    void Shoot()
    {
        //AUDIO shooting sound

        bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.AddComponent<MoveBullet>().speed = bulletSpeed;
        bulletInstance.GetComponent<MoveBullet>().lifetime = bulletLifetime;
    }
}
