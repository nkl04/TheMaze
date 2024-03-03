using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretQuesBox : MonoBehaviour
{

    public event EventHandler OnOpenSecretQuestion;
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


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(tagString))
        {
            //question canvas active
            OnOpenSecretQuestion?.Invoke(this,EventArgs.Empty);
            //question play

            //destroy this gameobject
            HideSelf();
        }   
    }

    private void HideSelf()
    {
        transform.gameObject.SetActive(false);
        
    }
}
