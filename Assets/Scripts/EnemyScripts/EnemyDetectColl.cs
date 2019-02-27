using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectColl : MonoBehaviour
{
    private int damageAmount;
    private int damageCoolDown;
    private bool coroutineStarted;

    private void Start()
    {
        coroutineStarted = false;
        damageAmount = 10;
        damageCoolDown = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")//check for being shot
            Destroy(this.gameObject);
        else if(other.gameObject.tag == "Player")//check for hurting player
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
        coroutineStarted = true;

        other.gameObject.GetComponentInChildren<PlayerHealth>().HurtPlayer(damageAmount);//take player health down by the amount (damage amount)

        yield return new WaitForSeconds(damageCoolDown);
        coroutineStarted = false;
    }
}
