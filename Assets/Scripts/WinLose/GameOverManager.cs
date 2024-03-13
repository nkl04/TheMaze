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
    

   private void Awake(){
        Instance = this;
        Hide();
        
        playAgainButton.onClick.AddListener(() =>{
            PlayAgain();
        });
        
        homeButton.onClick.AddListener(() =>{
            ReturnHome();
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
}
