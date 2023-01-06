using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    Transform playerPosition;
    Vector3 goToPoint;
    float step;
    bool canGo;

    void Start()
    {
        // Gets player position at time of spawning
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        canGo= true;
    }

    
    void Update()
    {
        // Moves ad/enemy towards player position at time of spawning
        step = speed * Time.deltaTime;
        if (canGo) transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("ENTER CIRCLE");
            canGo= false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("EXIT CIRCLE");
            canGo = true;
        }
    }
}
