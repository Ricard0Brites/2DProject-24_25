using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLogic : MonoBehaviour
{
    public SceneAsset FirstLevelScene;
    public void Quit()
    {
        Application.Quit();
    }   
    public void ShowSaveGameScreen()
    {
        Debug.Log("Showing Save Screen");
    }
    public void StartNewGame()
    {
        Debug.Log("Starting New Game");
        if (!FirstLevelScene)
            return;
        
        SceneManager.LoadScene(FirstLevelScene.name); 
    }
}
