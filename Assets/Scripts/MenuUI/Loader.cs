using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loading 
{
    public enum Scene{
        Map1,
        Map2, 
        Map3_Duongdemo,
        LoadingScene,
        MapOption,
        MainMenuScene

    }

    private static Scene targetScene;
    
    public static void Load(Scene targetScene){
        Loading.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }
    public static void LoaderCallBack(){
        SceneManager.LoadScene(targetScene.ToString());
    }

}