using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawning : MonoBehaviour
{
    public int spawnRate;
    public GameObject pickup;
    public GameObject altPickup;
    public AudioClip spawnSound;
    public AudioSource audioSource;
    void Start()
    {
        StartCoroutine(PickupTimer());
    }

    IEnumerator PickupTimer()
    {
        Vector2 pickupRandomSpawnPoint = new Vector2(Random.Range(-19, 19), Random.Range(-19, 19));
        Vector2 altPickupRandomSpawnPoint = new Vector2(Random.Range(-19, 19), Random.Range(-19, 19));

        pickup.transform.position = pickupRandomSpawnPoint;
        altPickup.transform.position = altPickupRandomSpawnPoint;

        pickup.gameObject.SetActive(true);
        altPickup.gameObject.SetActive(true);

        audioSource.PlayOneShot(spawnSound);

        yield return new WaitForSeconds(spawnRate);
        pickup.gameObject.SetActive(false);
        altPickup.gameObject.SetActive(false);

        StartCoroutine(PickupTimer());
    }
}
