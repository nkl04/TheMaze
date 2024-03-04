using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance {private set; get;}
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Transform revivePoint;
    [SerializeField] private bool reverseGravity = false;
    [SerializeField] private Canvas questionCanvas;
    [SerializeField] private Timer quizTimer;
    [SerializeField] private GameObject[] secretQuestionBoxArray;
    private bool canReverseGravity;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        player1.GetComponent<PlayerHealth>().OnPlayerDie += PlayerHealth_OnPlayerDie;
        player2.GetComponent<PlayerHealth>().OnPlayerDie += PlayerHealth_OnPlayerDie;
        foreach (GameObject questionBox in secretQuestionBoxArray)
        {
            questionBox.GetComponent<SecretQuesBox>().OnOpenSecretQuestion += QuestionBox_OnOpenSecretQuestion;
        }
        quizTimer.OnWaitingTimeOver += Timer_OnWaitingTimeOver;

    }

    private void Timer_OnWaitingTimeOver(object sender, EventArgs e)
    {
        questionCanvas.gameObject.SetActive(false);
        quizTimer.CancelTimer();
        
    }

    private void QuestionBox_OnOpenSecretQuestion(object sender, EventArgs e)
    {
        questionCanvas.gameObject.SetActive(true);
        quizTimer.StartTimeCounter();
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
