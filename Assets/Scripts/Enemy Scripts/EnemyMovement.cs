using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    GameObject playerCircle;
    Vector3 goToPoint;
    float step;
    bool canGo;

    void Start()
    {
        playerCircle = EnemyPool.SharedInstance.playerCircle;
        canGo= true;

    }

    
    void Update()
    {
        // Moves ad/enemy towards player position at time of spawning
        step = speed * Time.deltaTime;
        if (canGo) transform.position = Vector3.MoveTowards(transform.position, playerCircle.transform.position, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canGo= false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canGo = true;
        }
    }
}
