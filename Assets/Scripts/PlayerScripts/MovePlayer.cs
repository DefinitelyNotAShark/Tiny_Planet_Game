using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on the script from this tutorial
//https://www.youtube.com/watch?v=cH4oBoXkE00

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float fallVolume;

    [SerializeField]
    private AudioClip fallSound;

    private ParticleSystem fallParticles;
    private Vector3 moveDirection;
    private Rigidbody rigidbody;
    private Animator anim;
    private AudioSource audio;
    private TrailRenderer[] trails;

    private float horizontal;
    private float vertical;

    private bool playerHasFallenToPlanet;

    private CameraShake shake;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        fallParticles = GetComponentInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        trails = GetComponentsInChildren<TrailRenderer>();

        foreach (TrailRenderer t in trails)
            t.emitting = false;


        shake = GetComponentInChildren<Camera>().GetComponent<CameraShake>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;      
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection));//move

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)//if we're pushing move buttons     
            anim.SetBool("isMoving", true);//then the animator knows we moving
        
        else
            anim.SetBool("isMoving", false);//otherwise we're not moving        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerHasFallenToPlanet)
        {
            fallParticles.Play();
            StartCoroutine(shake.DoCameraShake(.1f));

            audio.PlayOneShot(fallSound, fallVolume);
            playerHasFallenToPlanet = true;//only plays once

            foreach (TrailRenderer t in trails)
                t.emitting = true;
        }
    }
}
