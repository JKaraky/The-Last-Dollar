using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    GameObject playerCircle;
    Vector3 goToPoint;
    bool canGo;
    Rigidbody2D enemyRb;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        playerCircle = EnemyPool.SharedInstance.playerCircle;
        canGo= true;
    }

    
    void FixedUpdate()
    {
        if (canGo)
        {
            Vector2 movementDirection = playerCircle.transform.position - transform.position;
            enemyRb.velocity = movementDirection * speed;
        }
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
