using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampToMiniMapEdge : MonoBehaviour
{
    public GameObject MinimapCamera;
    
    public float MinimapRadius;
    private Vector3 lastPos;
    private float distanceToCenter = 0.5f;

    private void Start()
    {
        MinimapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera");
    }

    void Update()
    {
        lastPos = transform.parent.transform.position;
        lastPos.y = transform.position.y;
        transform.position = lastPos;
    }

    void LateUpdate()
    {
        Vector3 centerPosition = MinimapCamera.transform.localPosition;
        centerPosition.y -= distanceToCenter;
        float Distance = Vector3.Distance(transform.position, centerPosition);

        // If the Distance is less than MinimapSize, it is within the Minimap view and we don't need to do anything
        // But if the Distance is greater than the MinimapSize, then do this
        if (Distance > MinimapRadius)
        {
            // Gameobject - Minimap
            Vector3 fromOriginToObject = transform.position - centerPosition;

            // Multiply by MinimapSize and Divide by Distance
            fromOriginToObject *= MinimapRadius / Distance;

            // Minimap + above calculation
            transform.position = centerPosition + fromOriginToObject;
        }
    }
}