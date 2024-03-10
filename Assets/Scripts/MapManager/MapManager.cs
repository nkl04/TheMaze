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
    [SerializeField] private LevelEntrance levelEntrance;
    [SerializeField] private GameObject finishPoint;
    private bool canReverseGravity;
    private ScoreKeeper scoreKeeper;

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
        levelEntrance.OnOutOfTheMap += LevelEntrance_OnOutOfTheMap;

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void LevelEntrance_OnOutOfTheMap(object sender, EventArgs e)
    {

        //unlock new level
        finishPoint.GetComponent<FinishPoint>().UnlockNewLevel();

        //loading the next level scene
        Loader.LoadTheNextScene();
    }

    private void Timer_OnWaitingTimeOver(object sender, EventArgs e)
    {
        questionCanvas.gameObject.SetActive(false);
        quizTimer.CancelTimer();
        Time.timeScale = 1;
        
    }

    private void QuestionBox_OnOpenSecretQuestion(object sender, EventArgs e)
    {
        questionCanvas.gameObject.SetActive(true);
        quizTimer.StartTimeCounter();
        Time.timeScale = 0;
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

        if (scoreKeeper.GetQuestionCollect() == secretQuestionBoxArray.Length)
        {
            //player get all secret box in the level
            levelEntrance.CanMove = true;
            //enable the levelentrance to take player to the next level
        }
        else
        {
            levelEntrance.CanMove = false;
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
