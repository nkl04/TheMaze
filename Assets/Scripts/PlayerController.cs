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
    [SerializeField] private float dashingPower = 10f;
    [SerializeField] TrailRenderer trailRenderer;

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
    private bool canDash = true;
    private bool isDashing = false;
    private float dashTime = 0.2f;
    private float dashingCooldown = 1f;



    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {   
        //if player dashing, stop all other control
        if(isDashing) return;

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
        if(!isGrounded) 
        {
            rb2d.velocity = new Vector2(airMoveSpeed * direction.x, rb2d.velocity.y);
        }
        else
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.right.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

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

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    //=================================================================================================================


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPoint.position,groundCheckSize);
    }

    
}
