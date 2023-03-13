using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollarFunctionality : MonoBehaviour
{
    [SerializeField] private PlayerCircle playerCircle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dollarTracker;
    [SerializeField] private GameObject dollarContainer;
    private GameObject enemy;
    private bool dollarBeingDrawn;
    private float xMovement;
    private float yMovement;
    public float attractionSpeed;
    public float recoverySpeed;
    public float rotationSpeed;
    public float maxDistanceFromPlayer;
    public float minDistanceFromPlayer;
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
            PlayerDollarControl();
        } else
        {
            NoForceOfAttaction();
        }
    }

    // The method that guides the dollar to the enemy
    public void ForceOfAttraction()
    {
        enemy = playerCircle.dollarEnemy;
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, attractionSpeed * Time.deltaTime);
    }

    public void NoForceOfAttaction ()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > maxDistanceFromPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, attractionSpeed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < minDistanceFromPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, dollarTracker.transform.position, attractionSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        }
    }

    public void PlayerDollarControl ()
    {
        if (Input.GetButtonDown("HorizontalDollar") || Input.GetButtonDown("VerticalDollar"))
        {
            xMovement = Input.GetAxisRaw("HorizontalDollar");
            yMovement = Input.GetAxisRaw("VerticalDollar");

            Vector2 inputVector = new Vector2(xMovement, yMovement).normalized;

            dollarContainer.transform.Translate(inputVector * recoverySpeed * Time.deltaTime);
        }
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
        if(collision.gameObject.GetComponent<EnemyMovement>() != null)
        {
            PlayerCircle.EnemyEnteredCircle -= CircleEnterListener;
            PlayerCircle.EnemyExitedCircle -= CircleExitListener;
            Destroy(gameObject);

            GameManager.Instance.UpdateGameState(GameState.End);
        }
        
    }
}
