using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This helped me find the rotation
//https://forum.unity.com/threads/script-that-lets-me-rotate-player-towards-velocity-direction-only-in-y-axis.296952/

public class SmoothRotate : MonoBehaviour
{
	void Update ()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float heading = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(0, heading, 0);
    }
}
