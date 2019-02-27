using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on the script from this tutorial
//https://www.youtube.com/watch?v=cH4oBoXkE00

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private ParticleSystem fallParticles;

    private Vector3 moveDirection;
    private Rigidbody rigidbody;
    private Animator anim;

    private float horizontal;
    private float vertical;

    private bool playerHasFallenToPlanet;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        fallParticles = GetComponentInChildren<ParticleSystem>();
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
            playerHasFallenToPlanet = true;//only plays once
        }
    }
}
