using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerLocation;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerLocation.position + offset;
    }
}
