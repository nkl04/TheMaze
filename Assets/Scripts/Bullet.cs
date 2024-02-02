using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private float damage;
    [SerializeField] private float fireSpeed = 10f;

    private Rigidbody2D rb2d;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * fireSpeed;
    }
    
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        DestroySelf();
    }    

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
