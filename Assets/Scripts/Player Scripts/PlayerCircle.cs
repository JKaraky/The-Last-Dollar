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
    public CircleCollider2D circleCollider;
    private ContactPoint2D[] enemyContact = new ContactPoint2D[30];

    private void OnCollisionEnter2D(Collision2D collision)
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
        else if (collision.gameObject.tag == "Alt Enemy")
        {
            // If there is already an enemy drawing the dollar nothing happens
            if (dollarEnemy != null)
            {
                return;
            }
            // If no enemy is drawing the dollar, this enemy draws it and it sends an event to DollarFunctionality to start moving
            else
            {
                collision.gameObject.tag = "Alt Dollar Enemy";
                dollarEnemy = collision.gameObject;
                EnemyEnteredCircle?.Invoke();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the dollar enemy dies, the dollar stops going to it via event sent to DollarFunctionality and the slot is allocated to a new enemy
        if (collision.gameObject.tag == "Dollar Enemy")
        {
            EnemyExitedCircle?.Invoke();

            // then we fill an array with whoever is touching the circle collider and set the closest distance to infinity and prepare a variable to hold the candidate
            int numberOfEnemiesInContact = circleCollider.GetContacts(enemyContact);

            if(numberOfEnemiesInContact == 0)
            {
                dollarEnemy.tag = "Enemy";
                dollarEnemy = null;
                return;
            } 
            else
            {
                // First we get the dollarEnemy location
                Vector2 dollarEnemyLocation = new Vector2(dollarEnemy.transform.position.x, dollarEnemy.transform.position.y);

                // And we set the distance from the enemy as big as possible and we prepare a variable to store the next dollar enemy candidate
                float closestDistance = Mathf.Infinity;
                GameObject dollarEnemyCandidate = null;

                // We check who amongst these is the closest to the dollarEnemy's last location
                for (int i = 0; i < numberOfEnemiesInContact; i++) {

                    Vector2 directionToEnemy = enemyContact[i].point - dollarEnemyLocation;

                    // This is for optimisation. It bypasses using the square root operation of doing Vector3.Distance
                    float dSqrToEnemy = directionToEnemy.sqrMagnitude;

                    if (dSqrToEnemy < closestDistance)
                    {
                        closestDistance = dSqrToEnemy;
                        dollarEnemyCandidate = enemyContact[i].collider.gameObject;
                    }
                }
                dollarEnemy.gameObject.tag = "Enemy";
                dollarEnemy = dollarEnemyCandidate;
                dollarEnemy.tag = "Dollar Enemy";
                EnemyEnteredCircle?.Invoke();
            }
        }
    }
}
