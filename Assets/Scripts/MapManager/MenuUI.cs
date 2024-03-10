using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
   [SerializeField] private Button playButton;
   [SerializeField] private Button quitButton;
   [SerializeField] private Button optionButton;
   private void Awake(){
        playButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.Level1Scene);
        });
        optionButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.SelectLevelScene);
        });
        quitButton.onClick.AddListener(() =>{
            Application.Quit();
        });

   }
}
