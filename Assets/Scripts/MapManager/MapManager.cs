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

    [SerializeField] private Canvas questionCanvas;
    [SerializeField] private Timer quizTimer;
    [SerializeField] private GameObject[] secretQuestionBoxArray;
    [SerializeField] private LevelEntrance levelEntrance;
    [SerializeField] private GameObject finishPoint;

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
        quizTimer.gameObject.SetActive(false);
        Time.timeScale = 1;
        player1.GetComponent<PlayerController>().CanMove = true;
        player2.GetComponent<PlayerController>().CanMove = true;
    }

    private void QuestionBox_OnOpenSecretQuestion(object sender, EventArgs e)
    {
        questionCanvas.gameObject.SetActive(true);
        quizTimer.gameObject.SetActive(true);
        quizTimer.StartTimeCounter();
        Time.timeScale = 0;
        PlayerController p1 = player1.GetComponent<PlayerController>();
        PlayerController p2 = player2.GetComponent<PlayerController>();
        p1.CanMove = false;
        p2.CanMove = false;
        p1.ResetVelocity();
        p2.ResetVelocity();
    }

    private void PlayerHealth_OnPlayerDie(object sender, EventArgs e)
    {
        //Revive players
        RevivePlayer();
        GameOverManager.Instance.Show();
        Time.timeScale = 0f;
        Pause.Instance.canPause = false;
        
    }

    private void Update() {
        
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

    

    public Canvas getQuesCanvas()
    {
        return questionCanvas;
    }
    private void RevivePlayer()
    {
        Debug.Log("Die!");
        player1.transform.position = revivePoint.position;
        player2.transform.position = revivePoint.position + new Vector3(0,0,2);
    }

    public GameObject[] GetPlayers()
    {
        GameObject[] playerArray = {player1,player2};
        return playerArray;
    }

}
