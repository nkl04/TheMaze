using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
   
   private bool keyPressed = false;
    void Update()
    {
        if (Input.anyKey && !keyPressed){
            keyPressed = true;
            GoToMainMenu();
        }
    }
    public void GoToMainMenu(){
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
