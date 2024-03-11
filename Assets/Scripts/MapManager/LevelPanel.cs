using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelPanel : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;



    private void Awake(){

        int unlockLevel = PlayerPrefs.GetInt("UnlockedLevel",1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }

        for (int i = 0; i < unlockLevel; i++)
        {
            levelButtons[i].interactable = true;
        }

        levelButtons[0].onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.Level1Scene);
        });
        levelButtons[1].onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.Level2Scene);
        });
        levelButtons[2].onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.Level3Scene);
        });


    } 
}
