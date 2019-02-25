using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [HideInInspector]
    public float speed, lifetime;

    private float elapsedTime;

    void FixedUpdate ()
    {
        elapsedTime += Time.deltaTime;

        Move();

        if (elapsedTime > lifetime)
            Destroy(this.gameObject);
	}

    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
