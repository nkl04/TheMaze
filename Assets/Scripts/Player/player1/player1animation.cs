using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1animation : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private const string SPEED = "Speed";
    private const string IS_JUMPING = "isJumping";
    private const string IS_TOUCHING_GROUND = "isTouchingGround";
    private const string IS_TOUCHING_WALL = "isTouchingWall";
    private const string VELOCITY_Y = "velocity Y";
    [SerializeField] private Animator animator;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask wallLayer;    
    public bool isTouchingWall = false;
    private float direction;
    public bool isFacingRight = true;

    public void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    public void Update() 
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        Moving();
    }

    public void Moving()
    {
        direction = Input.GetAxisRaw("Horizontal");

        animator.SetBool(IS_TOUCHING_WALL, isTouchingWall);
        
        animator.SetFloat(SPEED, direction*rb2D.velocity.x);
        
        animator.SetFloat(VELOCITY_Y, rb2D.velocity.y);

        animator.SetBool(IS_TOUCHING_GROUND, isTouchingGround);

        if(Input.GetKeyDown(KeyCode.W) && isTouchingGround == true)
        {   
            animator.SetBool(IS_JUMPING, true); 
        }
        
        // if ((isFacingRight && direction < 0) || (!isFacingRight && direction > 0))
        // {
        //     isFacingRight = !isFacingRight;
        //     Flip();
        // }
          
    }
    private void Flip() 
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
