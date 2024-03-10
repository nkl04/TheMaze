using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    
   [SerializeField] private Button Map1;
   [SerializeField] private Button Map2;
   [SerializeField] private Button Map3;
   [SerializeField] private Button cancelButton;
   private void Awake(){
        Map1.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.Level1Scene);
        });
        Map2.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.Level2Scene);
        });
        Map3.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.Level3Scene);
        });
        cancelButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.MainMenuScene);
        });

   }
}
