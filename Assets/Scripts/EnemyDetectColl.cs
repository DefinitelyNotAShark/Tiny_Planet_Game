using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectColl : MonoBehaviour
{
    private void Start()
    {
        //Destroy(this.gameObject, 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
            Destroy(this.gameObject);
    }
}
