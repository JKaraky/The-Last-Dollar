using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawning : MonoBehaviour
{
    [Header("Variables")]
    public static ProjectileSpawning projectilePoolInstance;
    public Projectile projectile;
    public int projectilesToSpawn;
    public ObjectPool<Projectile> projectilePool;
    public Transform playerLocation;

    void Awake()
    {
        projectilePoolInstance= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        projectilePool = new ObjectPool<Projectile>(
            CreateProjectile, ShootProjectile, RemoveProjectile, DestroyProjectile, true, 10, 20);
    }

    // To create projectiles for the pool
    public Projectile CreateProjectile()
    {
        return Instantiate(projectile);
    }

    // To show the projectile when it is shot
    public void ShootProjectile(Projectile projectile)
    {
        projectile.transform.position = new Vector2(playerLocation.position.x, playerLocation.position.y);
        projectile.gameObject.SetActive(true);
    }

    // To remove projectile when it hits something
    public void RemoveProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    // If the pool is full and a projectile is returned to it

    public void DestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}
