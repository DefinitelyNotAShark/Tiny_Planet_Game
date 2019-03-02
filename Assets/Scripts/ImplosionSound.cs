using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplosionSound : MonoBehaviour
{
    private AudioSource audio;

	void Start ()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(implosionCoolDown());
	}

    private IEnumerator implosionCoolDown()
    {
        yield return new WaitForSeconds(.5f);
        audio.Play();
    }
}
