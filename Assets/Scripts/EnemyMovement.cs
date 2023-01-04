using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    Vector3 playerPosition;
    float step;
    private float TimeToLive = 5f;

    void Start()
    {
        // Gets player position at time of spawning
        playerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        // Destroys ad/enemy after TimeToLive in seconds
        Destroy(gameObject, TimeToLive);
    }

    
    void Update()
    {
        // Moves ad/enemy towards player position at time of spawning
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
    }
}
