using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on the script from this tutorial
//https://www.youtube.com/watch?v=cH4oBoXkE00

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;


    private Vector3 moveDirection;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); ;
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection));//move
    }
}
