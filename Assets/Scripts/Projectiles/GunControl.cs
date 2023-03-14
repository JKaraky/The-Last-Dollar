using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunControl : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    public GameObject gun;
    public GameObject ammoContainer;
    public GameObject altAmmoContainer;
    public GameObject ammoFirstShot;
    public GameObject altAmmoFirstShot;
    public Vector3 mousePos;
    public int pickupAmmoReload;
    private int ammoLeft;
    private int altAmmoLeft;

    // Update is called once per frame
    void Update()
    {
        // To rotate gun
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotationVector = mousePos - gun.transform.position; // returns a vector pointing towards the mouse position

        float rotationZ = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg; // returns the angle of rotation towards the mouse position in degrees

        gun.transform.rotation = Quaternion.Euler(0, 0, rotationZ); // rotates the object accordingly

        // To shoot projectiles
        if (Input.GetMouseButtonDown(0) && !PauseMenu.gameIsPaused && ammoLeft != 0)
        {
            ProjectileSpawning.projectilePoolInstance.projectilePool.Get();
            ammoLeft--;

            if (ammoLeft == 1)
            {
                ammoFirstShot.SetActive(false);
            }
            else if (ammoLeft == 0)
            {
                ammoContainer.SetActive(false);
                ammoFirstShot.SetActive(true);
            }
        }

        // To shoot variant projectiles
        if (Input.GetMouseButtonDown(1) && !PauseMenu.gameIsPaused && altAmmoLeft != 0)
        {
            ProjectileSpawning.projectilePoolInstance.variantProjectilePool.Get();
            altAmmoLeft--;

            if (altAmmoLeft == 1)
            {
                altAmmoFirstShot.SetActive(false);
            }
            else if (altAmmoLeft == 0)
            {
                altAmmoContainer.SetActive(false);
                altAmmoFirstShot.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            ammoLeft = pickupAmmoReload;
            ammoContainer.SetActive(true);
            if(!ammoFirstShot.activeSelf)
            {
                ammoFirstShot.SetActive(true);
            }
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Alt Pickup")) {
            altAmmoLeft = pickupAmmoReload;
            altAmmoContainer.SetActive(true);
            if (!altAmmoFirstShot.activeSelf)
            {
                altAmmoFirstShot.SetActive(true);
            }
            collision.gameObject.SetActive(false);
        }
    }
}

