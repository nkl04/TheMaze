using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public const string IS_RUNNING = "IsRunning";
    public const string IS_JUMPING = "IsJumping";
    
    private Animator animator;
    private Vector2 direction;

    private void Start() {
        animator = this.GetComponent<Animator>();
    }
    public void Update() 
    {
        direction = GameInput.Instance.GetDirectionVector(gameObject.tag);
        animator.SetBool(IS_RUNNING, direction.x != 0);
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
