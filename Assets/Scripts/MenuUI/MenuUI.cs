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
            Loading.Load(Loading.Scene.Map1);
        });
        optionButton.onClick.AddListener(() =>{
            Loading.Load(Loading.Scene.MapOption);
        });
        quitButton.onClick.AddListener(() =>{
            Application.Quit();
        });

   }
}
