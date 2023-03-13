using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawning : MonoBehaviour
{
    public int spawnRate;
    public GameObject pickup;
    public GameObject altPickup;
    void Start()
    {
        StartCoroutine(PickupTimer());
    }

    IEnumerator PickupTimer()
    {
        Vector2 pickupRandomSpawnPoint = new Vector2(Random.Range(-13, 13), Random.Range(-13, 13));
        Vector2 altPickupRandomSpawnPoint = new Vector2(Random.Range(-13, 13), Random.Range(-13, 13));

        pickup.transform.position = pickupRandomSpawnPoint;
        altPickup.transform.position = altPickupRandomSpawnPoint;

        pickup.gameObject.SetActive(true);
        altPickup.gameObject.SetActive(true);
        yield return new WaitForSeconds(spawnRate);
        pickup.gameObject.SetActive(false);
        altPickup.gameObject.SetActive(false);
        StartCoroutine(PickupTimer());
    }
}
