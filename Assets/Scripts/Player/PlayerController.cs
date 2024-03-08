using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    

    public bool CanMove {get{return canMove;} set{canMove = value;}}
    
    [Header("Movement")]
    [SerializeField] private float airMoveSpeed = 5;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private bool canMove = true;

    [Header("Improvement")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    
    [Space(5)]

    [Header("Ground Check")]
    [SerializeField] private GroundCheck groundCheck;

    [Header("Elevator Stand")]
    [SerializeField] private LayerMask elevatorLayer;
    
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool canJump;
    private bool isJumping;
    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;

    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform.GetComponent<PlayerHealth>().OnPlayerDie += Health_OnPlayerDie;
    }

    private void Health_OnPlayerDie(object sender, EventArgs e)
    {
        rb2d.velocity = new Vector2(0f,0f);
    }

    private void Update()
    {   

        isGrounded = groundCheck.IsGrounded;

        #region movement && flip
        // //horizontal movement
        HorizontalMovement();
        //flip player when change direction
        if((isFacingRight && direction.x < 0) || (!isFacingRight && direction.x > 0))
        {
            isFacingRight = !isFacingRight;
            VerticalFlip();
        } 
        #endregion


        // Experience Improvement
        #region  Coyote Time & Jump Buffer 
        if(isGrounded)
        {  
            canJump = true;
            isJumping =false;
            coyoteTimeCounter = coyoteTime;      
        }
        else 
        {
            // player on the air
            if (isJumping)
            {
                // player jump
                canJump = false;
                jumpBufferTimeCounter -= Time.deltaTime;
            }
            else
            {
                // not jump but leave the ground
                coyoteTimeCounter -= Time.deltaTime;
                if (coyoteTimeCounter <= 0)
                {
                    canJump = false;  
                } 
            }
        }

        // if(jumpBufferTimeCounter > 0 && IsGrounded())
        // {
        //     VerticalMovement();
        //     canJump = false;
        //     isJumping = true;
        // }
        #endregion
        // Debug.Log(canJump + " " + isJumping + " ");

        Debug.Log(isGrounded);
    }
    
    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.GetComponent<Elevator>() && IsStandOnElevator())
    //     {
    //         transform.SetParent(other.transform);
    //     }
    // }


    // private void OnCollisionExit2D(Collision2D other) {
    //     if (other.gameObject.GetComponent<Elevator>())
    //     {
    //         transform.SetParent(null);
    //     }
    // }
    public void HorizontalMovement()
    {
        //change the velocity of the player
        if(!isGrounded)
        {
            rb2d.velocity = new Vector2(airMoveSpeed * direction.x, rb2d.velocity.y);

        }
        else rb2d.velocity = new Vector2(moveSpeed * direction.x, rb2d.velocity.y);
    }

    public void VerticalMovement()
    {
        //change the vetical position of player (Jump) 
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce * transform.up.y);
    }

    // public bool IsGrounded()
    // {
    //     return Physics2D.OverlapBox(new Vector2(groundCheckPoint.position.x, groundCheckPoint.position.y),groundCheckSize,0,groundCheckLayer);
    // }

    // public bool IsStandOnElevator()
    // {
    //     return Physics2D.OverlapBox(new Vector2(groundCheckPoint.position.x, groundCheckPoint.position.y),groundCheckSize,0,elevatorLayer);
    // }

    public void VerticalFlip()
    {
        transform.Rotate(0f,180f,0f);
    }

    public void HorizontalFlip()
    {
        transform.Rotate(180f,0f,0f);
    }


    //============================================ PLAYER INPUT SYSTEM ================================================
    public void OnMove(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            direction = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canMove)
        {   

            if (canJump && !isJumping)
            {
                canJump = false;
                isJumping = true;
                coyoteTimeCounter = 0f;
                VerticalMovement();

            }
            else
            {
                // player on the air
                jumpBufferTimeCounter = jumpBufferTime;
            }
        }
    }
    //=================================================================================================================

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(groundCheckPoint.position,groundCheckSize);
    // }

    public Vector2 GetDirectionVector()
    {
        return direction;
    }

    
    
}