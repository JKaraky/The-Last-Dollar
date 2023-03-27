using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D playerRb;
    private float xMovement;
    private float yMovement;
    [SerializeField] private float moveSpeed;

    [Header("Movement")]
    public float maxBorder;
    private SpriteRenderer sprite;
    [SerializeField] private Animator animator;


    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gameIsPaused)
        {
            Movement();
            //Constraints();
        }
    }

    void Movement()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        //Flipping the sprite based on movement
        if (xMovement < 0)
        {
            sprite.flipX = true;
        }
        else if (xMovement > 0)
        {
            sprite.flipX = false;
        }

        Vector2 inputVector = new Vector2(xMovement, yMovement);

        // For animation to know when we are walking

        animator.SetFloat("Speed", inputVector.sqrMagnitude);

        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        playerRb.velocity = inputVector * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Debug.Log("Homie entering");
        }
    }

    void Constraints()
    {
        float xMovementLimit = Mathf.Clamp(playerRb.position.x, -maxBorder, maxBorder);
        float yMovementLimit = Mathf.Clamp(playerRb.position.y, -maxBorder, maxBorder);
        playerRb.position = new Vector2(xMovementLimit, yMovementLimit);
    }
}
