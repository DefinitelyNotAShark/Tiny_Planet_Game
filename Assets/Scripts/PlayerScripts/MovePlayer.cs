using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on the script from this tutorial
//https://www.youtube.com/watch?v=cH4oBoXkE00

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3 moveDirection;
    private Rigidbody rigidbody;

    private enum Direction
    {
        left, 
        right, 
        forward,
        backward
    }

    private Direction playerDirection;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();;        
    }

    void Update ()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;
        UpdateDirection();
	}

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection));//move

        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * speed);
    }

    /// <summary>
    /// Changes the playerDirection enum depending on what input player is
    /// </summary>
    void UpdateDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(Mathf.Abs(horizontal) > Mathf.Abs(vertical))//if you're moving left or right more than up and down
        {
            if (horizontal >= 0)
                playerDirection = Direction.right;

            else
                playerDirection = Direction.left;

        }
        else if (Mathf.Abs(horizontal) < Mathf.Abs(vertical))//if you're moving Up or down more than left and right
        {
            if(vertical >= 0)
                playerDirection = Direction.forward;

            else
                playerDirection = Direction.backward;
        }
    }
}
