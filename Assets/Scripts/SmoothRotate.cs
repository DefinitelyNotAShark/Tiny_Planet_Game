using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotate : MonoBehaviour
{
	void Update ()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float heading = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(0, heading, 0);
        //Quaternion rotation = Quaternion.Euler(0, heading, 0);

        //transform.rotation = rotation;
    }
}
