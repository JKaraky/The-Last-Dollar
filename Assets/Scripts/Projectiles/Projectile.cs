using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int projectileSpeed;
    [SerializeField] private Rigidbody2D projectileRb;
    private Vector3 mousePos;
    private Camera mainCam;
    private float maxBorder = 14.5f;

    // Once the projectile is shot it just keeps going forward
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        projectileRb = GetComponent<Rigidbody2D>();
        Vector3 direction = mousePos - transform.position;
        projectileRb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
    }

    void Update()
    {
        // if the projectile goes out of bounds it is returned to the pool
        if (transform.position.x > maxBorder || transform.position.x < -maxBorder)
        {
            ProjectileSpawning.projectilePoolInstance.projectilePool.Release(this);
        }

        if (transform.position.y > maxBorder || transform.position.y < -maxBorder)
        {
            ProjectileSpawning.projectilePoolInstance.projectilePool.Release(this);
        }
    }

    // if the projectile hits an enemy, the enemy is set inactive and the projectile is returned to pool
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            ProjectileSpawning.projectilePoolInstance.projectilePool.Release(this);
        }
    }
}
