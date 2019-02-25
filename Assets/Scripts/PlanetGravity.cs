using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based off the script from this tutorial
//https://www.youtube.com/watch?v=cH4oBoXkE00

public class PlanetGravity : MonoBehaviour
{
    [SerializeField]
    private float gravity;

    private Rigidbody playerRigidbody;
    private Quaternion targetRotation;

    public void Attract(Transform playerTransform)
    {
        playerRigidbody = playerTransform.GetComponent<Rigidbody>();

        Vector3 gravityUp = (playerTransform.position - transform.position).normalized;
        Vector3 localUp = playerTransform.up;

        targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * playerTransform.rotation;
            
        playerRigidbody.AddForce(gravityUp * gravity);//add our gravity to the player so it doesn't fall off the planet
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 50f * Time.deltaTime);//HACK magic number rotates the player so it moves with the gravity
    }
}
