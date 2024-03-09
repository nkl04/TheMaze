using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOption : MonoBehaviour
{
    
   [SerializeField] private Button Map1;
   [SerializeField] private Button Map2;
   [SerializeField] private Button Map3;
   [SerializeField] private Button cancelButton;
   private void Awake(){
        Map1.onClick.AddListener(() =>{
            Loading.Load(Loading.Scene.Map1);
        });
        Map2.onClick.AddListener(() =>{
            Loading.Load(Loading.Scene.Map2);
        });
        Map3.onClick.AddListener(() =>{
            Loading.Load(Loading.Scene.Map3_Duongdemo);
        });
        cancelButton.onClick.AddListener(() =>{
            Application.Quit();
        });

   }
}
