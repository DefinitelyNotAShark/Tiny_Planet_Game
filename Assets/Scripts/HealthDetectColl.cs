using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDetectColl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IncreasePlayerHealth(other.gameObject);
        }
    }

    void IncreasePlayerHealth(GameObject player)
    {
        player.GetComponentInChildren<PlayerHealth>().HealPlayer(50);
        Destroy(this.gameObject);
    }

}
