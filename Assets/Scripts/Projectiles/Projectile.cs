using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int projectileSpeed;
    [SerializeField] private Rigidbody2D projectileRb;
    private Vector3 mousePos;
    private Camera mainCam;
    private Transform spawnPosition;
    private float maxBorder = 14.5f;


    void Awake()
    {
        // Finds camera and assigns rigidbody
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        projectileRb = GetComponent<Rigidbody2D>();
    }
    // Once the projectile is shot it just keeps going forward
    void OnEnable()
    {
        // Finds spawning position and assigns it along with where it should go
        spawnPosition = GameObject.Find("Spawning Position").GetComponent<Transform>();
        transform.position = spawnPosition.position;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Draws the vector the projectile will follow
        Vector3 direction = mousePos - spawnPosition.position;
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
