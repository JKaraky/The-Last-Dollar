using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVariant : MonoBehaviour
{
    [SerializeField] private int projectileSpeed;
    public Rigidbody2D projectileRb;
    private Transform spawnPosition;
    private Vector3 mousePos;
    private float maxBorder = 14.5f;


    void Awake()
    {
        // Assigns Rigidbody
        projectileRb = GetComponent<Rigidbody2D>();
    }
    // Once the projectile is shot it just keeps going forward
    void OnEnable()
    {
        // Assigns spawning location and mouse location from ProjectileSpawning script
        spawnPosition = ProjectileSpawning.projectilePoolInstance.spawningPosition;
        mousePos = ProjectileSpawning.projectilePoolInstance.mousepos;

        // Places projectile in position
        transform.position = spawnPosition.position;

        // Draws the vector the projectile will follow
        Vector3 direction = mousePos - spawnPosition.position;
        projectileRb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
    }

    void Update()
    {
        // if the projectile goes out of bounds it is returned to the pool
        if (transform.position.x > maxBorder || transform.position.x < -maxBorder)
        {
            ProjectileSpawning.projectilePoolInstance.variantProjectilePool.Release(this);
        }

        else if (transform.position.y > maxBorder || transform.position.y < -maxBorder)
        {
            ProjectileSpawning.projectilePoolInstance.variantProjectilePool.Release(this);
        }
    }

    // if the projectile hits an enemy, the enemy is set inactive and the projectile is returned to pool
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Alt Enemy" || collision.gameObject.tag == "Alt Dollar Enemy")
        {
            collision.gameObject.SetActive(false);
            ProjectileSpawning.projectilePoolInstance.variantProjectilePool.Release(this);
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Dollar Enemy")
        {
            ProjectileSpawning.projectilePoolInstance.variantProjectilePool.Release(this);
        }
    }
}
