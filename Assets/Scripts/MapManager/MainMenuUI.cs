using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject LevelPanel;
    
    [SerializeField] Button playButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button quitButton;
    

   AudioManager audioManager;
   private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            LevelPanel.SetActive(true);
        });
        optionButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
        });
        quitButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            Application.Quit();
        });
    }
}
