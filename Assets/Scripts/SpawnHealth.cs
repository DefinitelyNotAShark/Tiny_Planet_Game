﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPrefab;

	public void SpawnHealthPacks(Vector3 generatedPoint)
    {
        GameObject healthInstance = Instantiate(healthPrefab, generatedPoint, transform.rotation);
    }
}
