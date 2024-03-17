using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance {private set; get;}
    [SerializeField] private GameObject losingUI;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button continueButton;
    
    AudioManager audioManager;
    public GameObject levelPanel;

   private void Awake(){
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Hide();
        
        playAgainButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            PlayAgain();
        });
        
        homeButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            ReturnHome();
        });
         continueButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            AfterWinSelection();
        });
   }

    public void Hide()
    {
        losingUI.SetActive(false);
    }

    public void Show()
    {
        losingUI.SetActive(true);
    }

    public void PlayAgain()
    {
        Loader.Load(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ReturnHome()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
        Time.timeScale = 1f;
    }
    //Does not work?
    // public void AfterWinSelection(){
    //     Loader.Load(Loader.Scene.MainMenuScene);
    //     Time.timeScale = 1f;
    //     // GameObject.FindGameObjectWithTag("Level").SetActive(true);
    //     LevelPanel.SetActive(true);
    // }
 public void AfterWinSelection()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
        Time.timeScale = 1f;

        // Activate LevelPanel using GameManager
        if (Instance != null && Instance.levelPanel != null)
        {
            Instance.levelPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameManager or levelPanel reference is missing.");
        }
    }
}
