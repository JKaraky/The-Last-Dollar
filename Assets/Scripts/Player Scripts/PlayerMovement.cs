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
    private float maxBorder = 14.5f;


    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gameIsPaused)
        {
            Movement();
            Constraints();
        }
    }

    void Movement()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(xMovement, yMovement);

        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        playerRb.velocity = inputVector * moveSpeed;
    }

    void Constraints()
    {
        float xMovementLimit = Mathf.Clamp(playerRb.position.x, -maxBorder, maxBorder);
        float yMovementLimit = Mathf.Clamp(playerRb.position.y, -maxBorder, maxBorder);
        playerRb.position = new Vector2(xMovementLimit, yMovementLimit);
    }
}
