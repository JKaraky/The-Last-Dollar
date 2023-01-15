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
        playerPosition = EnemyPool.SharedInstance.playerPosition;
        canGo= true;

        // We subscribe to the PlayerCircle event
        PlayerCircle.EnemyEnteredCircle += CircleEnterListener;
        PlayerCircle.EnemyExitedCircle += CircleExitListener;

    }

    
    void Update()
    {
        // Moves ad/enemy towards player position at time of spawning
        step = speed * Time.deltaTime;
        if (canGo) transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, step);
    }

    public void CircleEnterListener()
    {
        canGo= false;
    }

    public void CircleExitListener ()
    {
        canGo = true;
    }

    private void OnDisable()
    {
        PlayerCircle.EnemyEnteredCircle -= CircleEnterListener;
        PlayerCircle.EnemyExitedCircle -= CircleExitListener;
    }
}
