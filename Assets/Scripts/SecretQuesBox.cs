using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretQuesBox : MonoBehaviour
{

    public enum Player{
        Player1,
        Player2,
    }

    [SerializeField] Canvas questionCanvas;
    [SerializeField] private Player player;
    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite blueSprite;

    private SpriteRenderer spriteRenderer;
    private String tagString;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player == Player.Player1)
        {
            spriteRenderer.sprite = redSprite;
            tagString = "Player1";
        }
        else
        {
            spriteRenderer.sprite = blueSprite;
            tagString = "Player2";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(tagString))
        {
            //question canvas active
            //question play

            //destroy this gameobject
            DestroySelf();
        }   
    }

    private void DestroySelf()
    {
        Destroy(transform.gameObject);
    }
}
