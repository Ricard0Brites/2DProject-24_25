using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLogic : MonoBehaviour
{
    public int FirstLevelSceneIndex = -1;
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
        if (FirstLevelSceneIndex > 0)
            SceneManager.LoadScene(FirstLevelSceneIndex); 
    }
    public void OpenSettings()
    {
        
    }
}
