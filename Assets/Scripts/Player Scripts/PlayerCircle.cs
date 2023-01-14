using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
    public GameObject dollarEnemy;
    public DollarFunctionality dollar;
    public static event Action EnemyEnteredCircle;
    public static event Action EnemyExitedCircle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if it collided with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // If there is already an enemy drawing the dollar nothing happens
            if (dollarEnemy != null)
            {
                return;
            }
            // If no enemy is drawing the dollar, this enemy draws it and it sends an event to DollarFunctionality to start moving
            else
            {
                collision.gameObject.tag = "Dollar Enemy";
                dollarEnemy = collision.gameObject;
                EnemyEnteredCircle?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the dollar enemy dies, a the dollar stops going to it via event sent to DollarFunctionality and the slot is emptied for a new enemy to fill
        if (collision.gameObject.tag == "Dollar Enemy")
        {
            dollarEnemy.gameObject.tag = "Enemy";
            dollarEnemy = null;
            EnemyExitedCircle?.Invoke();
        }
    }
}
