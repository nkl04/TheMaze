using System;
using System.Collections;
using System.Collections.Generic;
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

    private Vector3 vector3Up;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();     
    }
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform.GetComponent<PlayerHealth>().OnPlayerDie += Health_OnPlayerDie;
        if (ReverseGravityZone.Instance != null)
        {
            ReverseGravityZone.Instance.OnReverseGravity += ReverseGravityZone_OnReverseGravity;    
        }
        if (gameObject.tag == "Player1")
        {
            GameInput.Instance.GetPlayerInputSystem().Player1.Jump.performed += ctx => OnJump(ctx);
        }
        else
        {
            GameInput.Instance.GetPlayerInputSystem().Player2.Jump.performed += ctx => OnJump(ctx);
            
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
        direction = GameInput.Instance.GetDirectionVector(gameObject.tag);

        #region movement && flip
        // //horizontal movement
        HorizontalMovement();
        //flip player when change direction
        if((isFacingRight && direction.x < 0) || (!isFacingRight && direction.x > 0))
        {
            isFacingRight = !isFacingRight;
            VerticalFlip();
        } 

        Debug.Log(transform.eulerAngles);
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
        
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Moveable>() && IsStandOnElevator() && transform.parent == null)
        {
            transform.SetParent(other.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.GetComponent<Moveable>())
        {
            transform.SetParent(null);
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
        
        System.Random random = new System.Random();

        int randomNumber;
        if (transform.gameObject.tag == "Player1")
        {
            randomNumber = random.Next(1, 3);
        }
        else
        {
            randomNumber = random.Next(4, 5);
        }
        
        switch(randomNumber)
        {
            case 1:
            audioManager.PlaySFX(audioManager.jump1a);
            break;
            case 2:
            audioManager.PlaySFX(audioManager.jump1b);
            break;
            case 3:
            audioManager.PlaySFX(audioManager.jump1c);
            break;
            case 4:
            audioManager.PlaySFX(audioManager.jump2a);
            break;
            default:
            audioManager.PlaySFX(audioManager.jump2b);
            break;
        }
        
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

    public void horizontalFlip()
    {
        transform.Rotate(180f,0f,0f);
    }

    public void ResetVelocity()
    {
        rb2d.velocity = new Vector2(0f,0f);
    }




    //============================================ PLAYER INPUT SYSTEM ================================================

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

    public void SetDirectionVector(Vector2 vector)
    {
        direction = vector;
    }
    
    
}