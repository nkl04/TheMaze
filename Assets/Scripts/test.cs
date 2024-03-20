using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Rigidbody2D  rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReverseGravityZone();
        }
    }

    private void ReverseGravityZone()
    {
        transform.Rotate(180f,0f,0f);
        rb2d.gravityScale *= -1;
    }
}
