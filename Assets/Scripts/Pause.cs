using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public static Pause Instance {private set; get;}
    public GameObject pauseGameUI;
    public bool canPause = true;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button homeButton;
    private GameObject[] playerArray;

    private bool isPaused = false;

    private void Awake() {
        resumeButton.onClick.AddListener(() =>{
            ResumeGame();
        });
        // optionButton.onClick.AddListener(() =>{
        //     Loader.Load(SceneManager.GetActiveScene().buildIndex);
        // });
        
        homeButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    void Start()
    {
        Instance = this;
        pauseGameUI.SetActive(false);
        playerArray = MapManager.Instance.GetPlayers();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (!isPaused)
            {
                PauseScreen();
                foreach (GameObject player in playerArray)
                {
                    player.GetComponent<PlayerController>().CanMove = false;
                }
                isPaused = true;
            }
            else
            {
                ResumeGame();
                foreach (GameObject player in playerArray)
                {
                    player.GetComponent<PlayerController>().CanMove = true;
                }
                isPaused = false;
            }
        } 
    }
    public void PauseScreen()
    {
        Time.timeScale = 0f;
        pauseGameUI.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (!MapManager.Instance.getQuesCanvas().gameObject.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
        pauseGameUI.SetActive(false);
        isPaused = false;     
    }


    public void HomeScene()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
