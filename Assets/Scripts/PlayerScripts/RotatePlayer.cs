using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private enum Direction
    {
        left,
        upperLeft,
        lowerLeft,
        right,
        upperRight,
        lowerRight,
        forward,
        backward
    }

    private Direction playerDirection;

    void Update()
    {
        CheckDirection();
    }

    private void FixedUpdate()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        switch (playerDirection)
        {
            case Direction.forward: transform.localEulerAngles = new Vector3(0, 0, 0);  break;
            case Direction.right: transform.localEulerAngles = new Vector3(0, 90, 0); break;
            case Direction.left: transform.localEulerAngles = new Vector3(0, 270, 0); break;
            case Direction.backward: transform.localEulerAngles = new Vector3(0, 180, 0); break;
            case Direction.lowerRight: transform.localEulerAngles = new Vector3(0, 135, 0); break;
            case Direction.upperRight: transform.localEulerAngles = new Vector3(0, 45, 0); break;
            case Direction.lowerLeft: transform.localEulerAngles = new Vector3(0, 225, 0); break;
            case Direction.upperLeft: transform.localEulerAngles = new Vector3(0, 315, 0); break;
        }
    }


    /// <summary>
    /// Changes the playerDirection enum depending on what input player is
    /// </summary>
    void CheckDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) < Mathf.Abs(vertical))//if you're moving Up or down more than left and right
        {
            if (vertical > 0 && horizontal == 0)//forward
                playerDirection = Direction.forward;

            else if (vertical < 0 && horizontal == 0)//backward
                playerDirection = Direction.backward;
        }

        else if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))//if you're moving left or right more than up and down
        {
            if (horizontal > 0 && vertical == 0)//right
                playerDirection = Direction.right;

            else if (horizontal < 0 && vertical == 0)//left
                playerDirection = Direction.left;
        }

        else if(Mathf.Abs(horizontal) > 0 && Mathf.Abs(vertical) > 0)//diagonal check
        {
            if (horizontal > 0 && vertical > 0)//upperRight
                playerDirection = Direction.upperRight;

            else if (horizontal < 0 && vertical > 0)//upperLeft
                playerDirection = Direction.upperLeft;

            else if (horizontal < 0 && vertical < 0)//lowerLeft
                playerDirection = Direction.lowerLeft;

            else if (horizontal > 0 && vertical < 0)//lowerRight
                playerDirection = Direction.lowerRight;        
        }
    }
}