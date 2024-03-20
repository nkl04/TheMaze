using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;


public class controller1 : MonoBehaviour
{
    

    public bool CanMove {get{return canMove;} set{canMove = value;}}
    
    [Header("Movement")]
    [SerializeField] private float airMoveSpeed = 5;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private bool canMove = true;

    // [Header("Improvement")]
    // [SerializeField] private float coyoteTime = 0.2f;
    // [SerializeField] pr  ivate float jumpBufferTime = 0.2f;
    
    [Space(5)]

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundCheckLayer;

    [Header("Elevator Stand")]
    [SerializeField] private LayerMask elevatorLayer;
    
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private bool isFacingRight = true;
    // private bool canJump;
    // private bool isJumping;
    // private float coyoteTimeCounter;
    // private float jumpBufferTimeCounter;

    private Vector3 vector3Up;
    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform.GetComponent<PlayerHealth>().OnPlayerDie += Health_OnPlayerDie;
        if (ReverseGravityZone.Instance != null)
        {
            ReverseGravityZone.Instance.OnReverseGravity += ReverseGravityZone_OnReverseGravity;    
        }
    }

    private void ReverseGravityZone_OnReverseGravity(object sender, EventArgs e)
    {
        transform.SetParent(null);
    }

    private void Health_OnPlayerDie(object sender, EventArgs e)
    {
        rb2d.velocity = new Vector2(0f,0f);
    }

    private void Update()
    {   

        #region movement && flip
        // //horizontal movement
        HorizontalMovement();
        //flip player when change direction
        // if((isFacingRight && direction.x < 0) || (!isFacingRight && direction.x > 0))
        // {
        //     isFacingRight = !isFacingRight;
        //     VerticalFlip();
        // } 
        #endregion


        // Experience Improvement
        #region  Coyote Time & Jump Buffer 
        // if(IsGrounded())
        // {  
        //     canJump = true;
        //     isJumping =false;
        //     coyoteTimeCounter = coyoteTime;      
        // }
        // else 
        // {
        //     // player on the air
        //     if (isJumping)
        //     {
        //         // player jump
        //         canJump = false;
        //         jumpBufferTimeCounter -= Time.deltaTime;
        //     }
        //     else
        //     {
        //         // not jump but leave the ground
        //         coyoteTimeCounter -= Time.deltaTime;
        //         if (coyoteTimeCounter <= 0)
        //         {
        //             canJump = false;  
        //         } 
        //     }
        // }

        // if(jumpBufferTimeCounter > 0 && IsGrounded())
        // {
        //     VerticalMovement();
        //     canJump = false;
        //     isJumping = true;
        // }
        #endregion
        
         
        // if (transform.parent != null && transform.parent.up != vector3Up)
        // {
        //     transform.SetParent(null);
        // }

        Debug.Log(canMove);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Moveable>() && IsStandOnElevator())
        {
            transform.SetParent(other.transform);
            vector3Up = other.transform.up;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.GetComponent<Moveable>())
        {
            transform.SetParent(null);
            vector3Up = Vector3.zero;
        }
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
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce * transform.up.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(new Vector2(groundCheckPoint.position.x, groundCheckPoint.position.y),groundCheckSize,0,groundCheckLayer);
    }

    public bool IsStandOnElevator()
    {
        return Physics2D.OverlapBox(new Vector2(groundCheckPoint.position.x, groundCheckPoint.position.y),groundCheckSize,0,elevatorLayer);
    }

    public void VerticalFlip()
    {
        transform.Rotate(0f,180f,0f);
    }

    public void ResetVelocity()
    {
        rb2d.velocity = new Vector2(0f,0f);
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
            if(IsGrounded())
            {
               VerticalMovement(); 
            }
        }
    }
    //=================================================================================================================

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPoint.position,groundCheckSize);
    }

    public Vector2 GetDirectionVector()
    {
        return direction;
    }

    
    
}