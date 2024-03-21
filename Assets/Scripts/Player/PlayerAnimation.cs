using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    public const string IS_RUNNING = "IsRunning";
    public const string IS_JUMPING = "IsJumping";
    
    private Animator animator;
    private Vector2 direction;
    private bool isJumping;
    private bool isPressJump;
    private float previousVelocityY = 0;
    private PlayerController playerController;
    private Rigidbody2D rb2d;
    private void Start() {
        rb2d = this.GetComponent<Rigidbody2D>();
        previousVelocityY = rb2d.velocity.y;
        animator = this.GetComponent<Animator>();
        playerController = this.GetComponent<PlayerController>();
        if (gameObject.tag == "Player1")
        {
            GameInput.Instance.GetPlayerInputSystem().Player1.Jump.performed += ctx => JumpAnimation(ctx);
        }
        else
        {
            GameInput.Instance.GetPlayerInputSystem().Player2.Jump.performed += ctx => JumpAnimation(ctx);
            
        }
    }

    private void JumpAnimation(InputAction.CallbackContext ctx)
    {
        if (playerController.IsGrounded())
        {
            isJumping = true;
            isPressJump = true;
        }
        
    }

    public void Update() 
    {
        direction = GameInput.Instance.GetDirectionVector(gameObject.tag);

        if (isPressJump)
        {
            StartCoroutine(PressJumpCounter());
        }
        // if (rb2d.velocity.y > previousVelocityY)
        // {
        //     isJumping = true;
        // }

        //previousVelocityY = rb2d.velocity.y;
        if (playerController.IsGrounded() && !isPressJump)
        {
            isJumping = false;
        } 

        animator.SetBool(IS_RUNNING, direction.x != 0 && !isJumping);
        animator.SetBool(IS_JUMPING,isJumping);

        
    }

    IEnumerator PressJumpCounter()
    {
        yield return new WaitForSeconds(0.2f);
        isPressJump = false;
    }
    // public void Moving()
    // {
        

    //     //animator.SetBool(IS_TOUCHING_WALL, isTouchingWall);
        
    //     animator.SetFloat(SPEED, direction*rb2D.velocity.x);
        
    //     animator.SetFloat(VELOCITY_Y, rb2D.velocity.y);

    //     animator.SetBool(IS_TOUCHING_GROUND, isTouchingGround);

    //     if(Input.GetButtonDown("JumpPlayer2") && isTouchingGround)
    //     {   
    //         //animator.SetBool(IS_JUMPING, true); 
    //         rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    //     }

    // }
}
