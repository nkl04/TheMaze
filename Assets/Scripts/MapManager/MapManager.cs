using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Transform revivePoint;
    [SerializeField] private bool isReverseGravity = false;

    private bool canReverseGravity;
    // Start is called before the first frame update
    void Start()
    {
        player1.GetComponent<PlayerHealth>().OnPlayerDie += PlayerHealth_OnPlayerDie;
        player2.GetComponent<PlayerHealth>().OnPlayerDie += PlayerHealth_OnPlayerDie;
    }

    private void PlayerHealth_OnPlayerDie(object sender, EventArgs e)
    {
        //Revive players
        RevivePlayer();
    }

    private void ReverseGravity()
    {
        Rigidbody2D[] gameObjectRb2dArray = GameObject.FindObjectsOfType<Rigidbody2D>();
        foreach (Rigidbody2D gameobjectRb2d in gameObjectRb2dArray)
        {
            gameobjectRb2d.gravityScale = -gameobjectRb2d.gravityScale;
            if (gameobjectRb2d.gameObject.GetComponent<PlayerController>())
            {
                gameobjectRb2d.gameObject.GetComponent<PlayerController>().VerticalFlip();
            }
        }
    }




    private void RevivePlayer()
    {
        Debug.Log("Die!");
        player1.transform.position = revivePoint.position;
        player2.transform.position = revivePoint.position + new Vector3(0,0,2);
    }

    
}
