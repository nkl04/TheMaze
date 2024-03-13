using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float wallSlideSpeed = 2f;
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
    
    
    public void Update() 
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        Movement();
    }
     
    public void Movement()
    {
        direction = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(speed*direction, rb2D.velocity.y);

        if(Input.GetButtonDown("Jump") && isTouchingGround)
        {   
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);  
        }
        
        // if ((isFacingRight && direction < 0) || (!isFacingRight && direction > 0))
        // {
        //     if (isTouchingWall == true && direction != 0)
        //     {
        //         rb2D.velocity = new Vector2(0, -5f);               
        //     }
        // }       

        if (isTouchingWall && !isTouchingGround && direction != 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        
    }
    
    
    



}
