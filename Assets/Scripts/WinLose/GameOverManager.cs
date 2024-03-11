using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
  [SerializeField] private Button Restart;
   [SerializeField] private Button MainMenu;
   [SerializeField] private Button Quit;

   private void Awake(){
        Restart.onClick.AddListener(() =>{
            Loading.Load(Loading.Scene.Map1);
        });
        
        MainMenu.onClick.AddListener(() =>{
            Loading.Load(Loading.Scene.MainMenuScene);
        });

        Quit.onClick.AddListener(() =>{
            Application.Quit();
        });
   }
}
