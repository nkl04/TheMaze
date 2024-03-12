using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public static Pause Instance {private set; get;}
    public GameObject pauseGameUI;
    public bool canPause = true;
    private bool isPaused = false;

    void Start()
    {
        Instance = this;
        pauseGameUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (!isPaused)
            {
                PauseScreen();
                isPaused = true;
            }
            else
            {
                ResumeGame();
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
