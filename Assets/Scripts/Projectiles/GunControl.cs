using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    public Vector3 mousePos;

    // Update is called once per frame
    void Update()
    {
        // To rotate gun
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotationVector = mousePos - transform.position; // returns a vector pointing towards the mouse position

        float rotationZ = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg; // returns the angle of rotation towards the mouse position in degrees

        transform.rotation = Quaternion.Euler(0, 0, rotationZ); // rotates the object accordingly

        // To shoot projectiles
        if (Input.GetMouseButtonDown(0) && !PauseMenu.gameIsPaused)
        {
            ProjectileSpawning.projectilePoolInstance.projectilePool.Get();
        }
    }
}

