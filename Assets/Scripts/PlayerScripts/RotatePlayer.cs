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

        Debug.Log("Direction = " + moveDirection);
    }


    private void FixedUpdate()
    {
        UpdateDirection();
        // transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
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
            {
                playerDirection = Direction.right;
                transform.localEulerAngles = new Vector3(0, 90, 0);
            }

            else
            {
                playerDirection = Direction.left;
                transform.localEulerAngles = new Vector3(0, 270, 0);
            }

        }
        else if (Mathf.Abs(horizontal) < Mathf.Abs(vertical))//if you're moving Up or down more than left and right
        {
            if (vertical >= 0)
            {
                playerDirection = Direction.forward;
                transform.localEulerAngles = new Vector3(0, 0, 0);

            }

            else
            {
                playerDirection = Direction.backward;
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
        }
    }
}
