using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab of the ad/enemy
    public Transform adMen;
    public float spawnRate;
    // List of spawn points
    public List<Transform> spawners;
    int randomNumber;
    void Start()
    {
        StartCoroutine(spawnTimer(randomNumber));
    }

    
    void Update()
    {
        // Randomly selecting a spawn point
        randomNumber = Random.Range(0, spawners.Count);
    }

    IEnumerator spawnTimer(int number)
    {
        // Spawner creates an enemy at the randomly chosen spawn point
        yield return new WaitForSeconds(spawnRate);
        Instantiate(adMen, spawners[randomNumber].position, spawners[randomNumber].rotation);
        StartCoroutine(spawnTimer(randomNumber));
    }
}
