using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [HideInInspector]
    public float speed, lifetime;

    private float elapsedTime;
    public GameObject muzzlePrefab, hitPrefab;
    private GameObject hitFX;
    private void Awake()
    {
        if (muzzlePrefab != null)
        {
            var muzzleFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleFX.transform.forward = gameObject.transform.forward;
        }
    }
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

    //TODO: bullet instantly destroys itself, keep commented until fix
    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        ContactPoint cont = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, cont.normal);
        Vector3 pos = cont.point;
        if (hitPrefab != null)
        {
            hitFX = Instantiate(hitPrefab, pos, rot);
            hitFX.transform.forward = gameObject.transform.forward;
        }
        Destroy(this.gameObject);
    }
}
