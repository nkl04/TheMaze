using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string IS_RUNNING = "isRunning";
    [SerializeField] private float speed = 10f; 
    [SerializeField] private Animator animator;
    private Rigidbody2D rb2D;

    public void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private float direction;
    private bool isFacingRight = true;
    
    public void Update() 
    {
        Movement();
    }
    private void Movement(){
        direction = Input.GetAxisRaw("Horizontal");
        
        rb2D.velocity = new Vector2(speed*direction, rb2D.velocity.y);


        if ((isFacingRight && direction < 0) || (!isFacingRight && direction > 0))
        {
            isFacingRight = !isFacingRight;
            Flip();
        }
        animator.SetBool(IS_RUNNING, direction != 0);
        
    }
    private void Flip() 
    {
        transform.Rotate(0f, 180f, 0f);
    }





}
