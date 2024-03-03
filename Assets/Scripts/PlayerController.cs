using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private const string IS_RUNNING = "isRunning";
    private const string VECTOR_Y = "Vector y";
    private const string IS_JUMPING = "isJumping";
    private const string IS_TOUCHING_GROUND = "isTouchingGround";
    private const string IS_TOUCHING_WALL = "isTouchingWall";
    [SerializeField] private float speed = 5f; 
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpSpeed = 5f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask wallLayer;    
    public bool isTouchingWall = false;
    public Rigidbody2D rb2D;
    //public float VectorY;


    public void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private float direction;
    private bool isFacingRight = true;
    
    public void Update() 
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        Movement();
        //VectorY = ;
    }
     
    private void Movement()
    {
        direction = Input.GetAxisRaw("Horizontal");

        rb2D.velocity = new Vector2(speed*direction, rb2D.velocity.y);
        
        //animator.SetFloat("Vector y", VectorY);
        
        animator.SetBool(IS_TOUCHING_WALL, false);

        if(Input.GetButtonDown("Jump"))
        {   
            animator.SetBool(IS_JUMPING, true);             
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);  
            animator.SetBool(IS_TOUCHING_GROUND, false);              
        }
        
        if ((isFacingRight && direction < 0) || (!isFacingRight && direction > 0))
        {
            isFacingRight = !isFacingRight;
            Flip();
        }
          
        

        if (rb2D.velocity.y < jumpSpeed )
            animator.SetBool(IS_JUMPING, false);

        if (rb2D.velocity.y == 0)       
            animator.SetBool(IS_TOUCHING_GROUND, true);
        
        
        if (isTouchingWall == true)
        {
            animator.SetBool(IS_TOUCHING_WALL, true);
            if (isFacingRight == false) Flip();
        }

        animator.SetBool(IS_RUNNING, direction != 0);
       
        
    }
    private void Flip() 
    {
        transform.Rotate(0f, 180f, 0f);
    }
    
    



}
