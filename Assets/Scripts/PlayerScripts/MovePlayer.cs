using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on the script from this tutorial
//https://www.youtube.com/watch?v=cH4oBoXkE00

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed=20f,boostSpeed=40f;
    private bool isBoosted=true;
    Vector2 inputDir;
    [SerializeField] private float speedSmoothTime = .1f;
    private float speedSmoothVelocity;
    private float currentSpeed;

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

    [SerializeField] private float turnSmoothTime = .2f;
    private float turnSmoothVelocity;

    private bool playerHasFallenToPlanet;

    private CameraShake shake;
    private float yaw, pitch;
    [SerializeField] private Transform target, player, cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
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
        MovementInput();

    }
    private void LateUpdate()
    {
        //yaw += Input.GetAxis("Mouse X");
        //pitch -= Input.GetAxis("Mouse Y");

        //Vector3 targetRotation = new Vector3(pitch, yaw);
        //transform.eulerAngles = targetRotation;
    }
    private void MovementInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //inputDir = input.normalized;
        //if (inputDir != Vector2.zero)
        //{
        //    float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        //    transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        //}
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        moveDirection = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection));//move

        //float targetSpeed = ((isBoosted) ? boostSpeed : moveSpeed) * inputDir.magnitude;
        //currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        //transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

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
