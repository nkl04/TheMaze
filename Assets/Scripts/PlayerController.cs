using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 10;

    private Rigidbody2D rb2d;
    private Vector2 direction;

    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        //change the velocity of the player
        rb2d.velocity = new Vector2(moveSpeed * direction.x, rb2d.velocity.y);
    }

    private void VerticalMovement()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            VerticalMovement();
        }
    }
}
