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
    [SerializeField] private bool reverseGravity = false;

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

    private void Update() {
        if (reverseGravity)
        {
            if (canReverseGravity)
            {
                ReverseGravity(player1);
                ReverseGravity(player2);
                canReverseGravity = false;
            }
        }
        else
        {
            canReverseGravity = true;
            ResetGravity(player1);
            ResetGravity(player2);
        }
    }

    private void ReverseGravity(GameObject gameObject)
    {
        float gravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = -gravity;
        gameObject.transform.GetComponent<PlayerController>().HorizontalFlip();
    }

    private void ResetGravity(GameObject gameObject)
    {
        float gravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = Mathf.Abs(gravity);
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }


    private void RevivePlayer()
    {
        Debug.Log("Die!");
        player1.transform.position = revivePoint.position;
        player2.transform.position = revivePoint.position + new Vector3(0,0,2);
    }

    
}
