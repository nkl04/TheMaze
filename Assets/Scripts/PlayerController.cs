using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 10;

    [Header("Improvement")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    

    [Space(5)]

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded; 
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundCheckLayer;

    [Header("Fire")]
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform gunHandler;
    
    

    private Rigidbody2D rb2d;
    private Vector2 direction;
    private bool isFacingRight = true;
    private bool canJump;
    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;

    private StateMachine stateMachine;


    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine(this);
        stateMachine.Initialize(stateMachine.IdleState);
    }

    private void Update()
    {   
        //horizontal movement
        HorizontalMovement();


        //flip player when change direction
        if((isFacingRight && direction.x < 0) || (!isFacingRight && direction.x > 0))
        {
            isFacingRight = !isFacingRight;
            Flip();
        } 

        //check if player is grounded
        isGrounded = IsGrounded();


        // Experience Improvement
        #region  Coyote Time & Jump Buffer 
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        jumpBufferTimeCounter -= Time.deltaTime;

        if(jumpBufferTimeCounter > 0 && canJump)
        {
            VerticalMovement();
        }

        #endregion


    
    }

    private void HorizontalMovement()
    {
        //change the velocity of the player
        rb2d.velocity = new Vector2(moveSpeed * direction.x, rb2d.velocity.y);
    }

    private void VerticalMovement()
    {
        //change the vetical position of player (Jump) 
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefabs,gunHandler.position,gunHandler.rotation);
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
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            if(canJump)
            {
                VerticalMovement();
            }
            else 
            {
                //on the air
                jumpBufferTimeCounter = jumpBufferTime;
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            //Fire
            Fire();
        }
    }

    //=================================================================================================================


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPoint.position,groundCheckSize);
    }

    
}
