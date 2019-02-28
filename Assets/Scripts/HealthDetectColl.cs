using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDetectColl : MonoBehaviour
{

    private AudioSource audio;
    private Renderer[] renderers;
    private Collider[] colliders;

    private bool coroutineStarted;

    private void Start()
    {
        coroutineStarted = false;
        audio = GetComponent<AudioSource>();
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!coroutineStarted)
                StartCoroutine(DestroyCoolDown(other.gameObject));
        }
    }

    IEnumerator DestroyCoolDown(GameObject player)
    {
        coroutineStarted = true;

        foreach (Renderer r in renderers)
            r.enabled = false;//make untouchable

        audio.Play();//play the get health sound so the player knows something happened
        player.GetComponentInChildren<PlayerHealth>().HealPlayer(50);//give player back 50 health

        foreach (Collider c in colliders)
            c.enabled = false;//make uncollidable

        yield return new WaitForSeconds(2);//wait until the sound is finished playing
        Destroy(this.gameObject);//then destroy
    }
}
