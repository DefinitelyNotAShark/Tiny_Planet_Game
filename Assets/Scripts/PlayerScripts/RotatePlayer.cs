using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    private Vector3 moveDirection;
    private Vector3 origin;


    private enum Direction
    {
        left,
        right,
        forward,
        backward
    }

    private float yRotation;
    private Direction playerDirection;

    void Update()
    {
        moveDirection = GetComponentInParent<MovePlayer>().moveDirection;
        UpdateDirection();

        Debug.Log("Direction = " + moveDirection);
    }


    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        origin = Vector3.zero;

        Quaternion horizontalQuat = Quaternion.AngleAxis(-horizontal, Vector3.up);
        Quaternion verticalQuat = Quaternion.AngleAxis(vertical, Vector3.right);

        Quaternion rotateQuat = horizontalQuat * verticalQuat;

        transform.localRotation = rotateQuat;

    }
/// <summary>
/// Changes the playerDirection enum depending on what input player is
/// </summary>
void UpdateDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))//if you're moving left or right more than up and down
        {
            if (horizontal >= 0)
                playerDirection = Direction.right;

            else
                playerDirection = Direction.left;

        }
        else if (Mathf.Abs(horizontal) < Mathf.Abs(vertical))//if you're moving Up or down more than left and right
        {
            if (vertical >= 0)
                playerDirection = Direction.forward;

            else
                playerDirection = Direction.backward;
        }
    }
}
