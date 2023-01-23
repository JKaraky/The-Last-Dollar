using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollarFunctionality : MonoBehaviour
{
    [SerializeField] private PlayerCircle playerCircle;
    private GameObject enemy;
    private bool dollarBeingDrawn;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the event in PlayerCircle to know when an enemy collides and leaves the circle
        PlayerCircle.EnemyEnteredCircle += CircleEnterListener;
        PlayerCircle.EnemyExitedCircle += CircleExitListener;

        dollarBeingDrawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dollarBeingDrawn)
        {
            ForceOfAttraction();
        }
    }

    // The method that guides the dollar to the enemy
    public void ForceOfAttraction()
    {
        enemy = playerCircle.dollarEnemy;
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
    }

    // Called when only when there is no Enemy drawing the dollar
    public void CircleEnterListener()
    {
        dollarBeingDrawn = true;
    }

    // Called only when enemy drawing the dollar leaves the circle
    public void CircleExitListener()
    {
        dollarBeingDrawn = false;
    }

    // When colliding with any enemy, the dollar is lost, we unsibscribe from all events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Dollar Enemy")
        {
            PlayerCircle.EnemyEnteredCircle -= CircleEnterListener;
            PlayerCircle.EnemyExitedCircle -= CircleExitListener;
            Destroy(gameObject);
        }
        
    }
}
