using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject pauseGameUI;
    public void pauseScreen()
    {
        Time.timeScale = 0f;
        pauseGameUI.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void resumeGame()
    {
        pauseGameUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
