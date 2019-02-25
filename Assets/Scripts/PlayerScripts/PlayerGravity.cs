using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based off of the script from this tutorial
//https://www.youtube.com/watch?v=cH4oBoXkE00/

public class PlayerGravity : MonoBehaviour
{
    private PlanetGravity planetPlayerIsOn;

    private Rigidbody rigidbody;


	void Start ()
    {
        planetPlayerIsOn = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetGravity>();

        rigidbody = GetComponent<Rigidbody>();

        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	

	void FixedUpdate ()
    {
        if (planetPlayerIsOn)
        {
            planetPlayerIsOn.Attract(transform);
        }
	}
}
