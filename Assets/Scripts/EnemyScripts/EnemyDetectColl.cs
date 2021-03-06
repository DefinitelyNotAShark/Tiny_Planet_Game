﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectColl : MonoBehaviour
{
    [HideInInspector]
    public GameObject implosion;

    private int damageAmount;
    private int damageCoolDown;
    private bool coroutineStarted;
    private bool enemyIsDead;

    private Rigidbody rigidbody;

    private ParticleSystem hitParticles;
    private GameManager manager;

    private void Start()
    {     
        coroutineStarted = false;
        damageAmount = 10;
        damageCoolDown = 1;

        rigidbody = GetComponent<Rigidbody>();
        hitParticles = GetComponentInChildren<ParticleSystem>();      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")//check for being shot
        {
            manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//decrease enemy amount

            if (!enemyIsDead)//if the enemy is not already going through death 
            {
                manager.DecreaseEnemyAmount(1);//HACK program in a point amount later
                enemyIsDead = true;//set the enemy to be going through death


                if (implosion != null)
                {
                    var implosionInstance = Instantiate(implosion, transform.position, Quaternion.identity);
                    implosionInstance.transform.forward = gameObject.transform.forward;
                }

                Destroy(this.gameObject);
            }
        }


        else if (other.gameObject.tag == "Player")//check for hurting player
        {
            StartCoroutine(HurtPlayer(other));//hurt them
        }
    }

    private void OnTriggerStay(Collider other)//player can also be hurt if they stay in contact with the razor for after their cooldown
    {
         if (other.gameObject.tag == "Player")//check for hurting player
        {
            if (!coroutineStarted)//if we're not already hurting the player
                StartCoroutine(HurtPlayer(other));//hurt them
        }
    }

    private IEnumerator HurtPlayer(Collider other)
    {
        hitParticles.Play();
        coroutineStarted = true;
        DoKnockBack(other);//pass in the player coll so it can get the player position
        other.gameObject.GetComponentInChildren<PlayerHealth>().HurtPlayer(damageAmount);//take player health down by the amount (damage amount)

        yield return new WaitForSeconds(damageCoolDown);
        coroutineStarted = false;
    }

   void DoKnockBack(Collider playerColl)
    {
        Transform playerTransform = playerColl.gameObject.transform;

        Vector3 direction = transform.position - playerTransform.position;
        rigidbody.AddForce(direction.normalized * 1000f);
    }
}
