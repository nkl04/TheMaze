using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject pauseGameUI;
    public bool isPaused = false;

    void Start()
    {
        pauseGameUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pauseScreen(); 
    }
    public void pauseScreen()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseGameUI.SetActive(true);
    }

    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pauseGameUI.SetActive(false); 
        isPaused = false;       
    }

    public void HomeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
