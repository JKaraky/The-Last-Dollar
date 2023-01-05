using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
        GameObject adMen = EnemyPool.SharedInstance.GetPooledObject();
        if (adMen != null)
        {
            adMen.transform.position = spawners[randomNumber].position;
            adMen.transform.rotation= spawners[randomNumber].rotation;
            adMen.SetActive(true);
        }
        StartCoroutine(spawnTimer(randomNumber));
    }
}
