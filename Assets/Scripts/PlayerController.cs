using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float airMoveSpeed = 5;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 10;

    [Header("Improvement")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    
    [Space(5)]

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundCheckLayer;

    
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private bool isFacingRight = true;
    private bool canJump;
    private bool isJumping;
    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;
    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {   

        // //horizontal movement
         HorizontalMovement();

        //flip player when change direction
        if((isFacingRight && direction.x < 0) || (!isFacingRight && direction.x > 0))
        {
            isFacingRight = !isFacingRight;
            Flip();
        } 

        // Experience Improvement
        #region  Coyote Time & Jump Buffer 
        if(IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            canJump = true;
            isJumping = false;  
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

        

        Debug.Log(canJump + " " + isJumping);

        if(jumpBufferTimeCounter > 0 && IsGrounded())
        {
            VerticalMovement();
            canJump = false;
            isJumping = true;
            Debug.Log("Buffer jump");
        }
        #endregion

    }

    public void HorizontalMovement()
    {
        //change the velocity of the player
        if(!IsGrounded())
        {
            rb2d.velocity = new Vector2(airMoveSpeed * direction.x, rb2d.velocity.y);

        }
        else rb2d.velocity = new Vector2(moveSpeed * direction.x, rb2d.velocity.y);
    }

    public void VerticalMovement()
    {
        //change the vetical position of player (Jump) 
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(new Vector2(groundCheckPoint.position.x, groundCheckPoint.position.y),groundCheckSize,0,groundCheckLayer);
    }

    public void Flip()
    {
        transform.Rotate(0f,180f,0f);
    }


    //============================================ PLAYER INPUT SYSTEM ================================================
    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {   
            if(rb2d.velocity.y > 0)
            {
                return;
            }
            if (canJump && !isJumping)
            {
                VerticalMovement();
                canJump = false;
                isJumping = true;
            }
            else
            {
                // player on the air
                jumpBufferTimeCounter = jumpBufferTime;
            }
        }
    }
    //=================================================================================================================

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPoint.position,groundCheckSize);
    }
}
