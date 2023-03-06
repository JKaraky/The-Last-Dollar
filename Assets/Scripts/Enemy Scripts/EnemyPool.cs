using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

// Manages enemy pool and keeps track of player position so the enemy can head towards its position
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> altPooledObjects;
    public GameObject objectToPool;
    public GameObject altObjectToPool;
    public int amountToPool;
    public GameObject playerCircle;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        // Making the first enemy pool
        pooledObjects= new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

        // Making the alt enemy pool
        altPooledObjects = new List<GameObject>();
        GameObject altTmp;
        for (int i = 0; i < amountToPool; i++)
        {
            altTmp = Instantiate(altObjectToPool);
            altTmp.SetActive(false);
            altPooledObjects.Add(altTmp);
        }
    }

    public GameObject GetPooledObject()
    {
        int randomNumber = UnityEngine.Random.Range(0, 2);

        if(randomNumber == 0)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!altPooledObjects[i].activeInHierarchy)
                {
                    return altPooledObjects[i];
                }
            }
        }
        return null;
    }
}
