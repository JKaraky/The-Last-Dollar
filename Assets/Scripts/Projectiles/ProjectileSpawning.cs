using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

// This script manages the projectile pool and saves variables essential to be used in the Projectile script (Spawning position, mouse location)
public class ProjectileSpawning : MonoBehaviour
{
    [Header("Variables for the Pool")]
    public static ProjectileSpawning projectilePoolInstance;
    public ObjectPool<Projectile> projectilePool;
    public ObjectPool<ProjectileVariant> variantProjectilePool;

    [Header("Variable for the Projectile")]
    public Projectile projectile;
    public ProjectileVariant projectileVariant;
    [SerializeField] Camera mainCam;
    public Transform spawningPosition;
    public Vector3 mousepos;

    void Awake()
    {
        projectilePoolInstance= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        projectilePool = new ObjectPool<Projectile>(
            CreateProjectile, ShootProjectile, RemoveProjectile, DestroyProjectile, true, 10, 20);

        variantProjectilePool = new ObjectPool<ProjectileVariant>(
            CreateVariantProjectile, ShootVariantProjectile, RemoveProjectileVariant, DestroyProjectileVariant, true, 10, 20);
    }

    // To know where the mouse location is for the projectile to head towards it
    void Update()
    {
        mousepos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    // To create projectiles for the pool
    public Projectile CreateProjectile()
    {
        return Instantiate(projectile);
    }

    // To show the projectile when it is shot
    public void ShootProjectile(Projectile projectile)
    {
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

    // Same previous methods but for the variant projectile

    public ProjectileVariant CreateVariantProjectile()
    {
        return Instantiate(projectileVariant);
    }

    // To show the projectile when it is shot
    public void ShootVariantProjectile(ProjectileVariant projectileVariant)
    {
        projectileVariant.gameObject.SetActive(true);
    }

    // To remove projectile when it hits something
    public void RemoveProjectileVariant(ProjectileVariant projectileVariant)
    {
        projectileVariant.gameObject.SetActive(false);
    }

    // If the pool is full and a projectile is returned to it

    public void DestroyProjectileVariant(ProjectileVariant projectileVariant)
    {
        Destroy(projectileVariant.gameObject);
    }
}
