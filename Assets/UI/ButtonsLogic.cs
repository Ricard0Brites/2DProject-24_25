using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsLogic : MonoBehaviour
{
    public int FirstLevelSceneIndex = -1;
    public int MainMenuSceneIndex = -1;

    public GameObject SavePanelContainer = null;
    public GameObject SaveButtonsContainer = null;

	private void Start()
	{
        // Hide the Save Panel (done here for convenience)
        if (SavePanelContainer)
            SavePanelContainer.SetActive(false);

        //Assign Found Saves to the buttons
        if(SaveButtonsContainer)
        {
            Text[] SaveButtonsTextComponent = SaveButtonsContainer.GetComponentsInChildren<Text>();
            SaveButton[] SaveButtonsComponent = SaveButtonsContainer.GetComponentsInChildren<SaveButton>();
            int NumberOfSavesFound = SaveSystem.GetNumberOfSaves(), Index = 0;
            SaveFileData[] SaveDataFound = SaveSystem.GetAllSavesInfo();

		    if (NumberOfSavesFound > 0 && NumberOfSavesFound < 5)
            {
                foreach(Text TextComponent in SaveButtonsTextComponent)
                {
                    TextComponent.text = SaveDataFound[Index].CreationDateTime.ToString();

                    SaveButtonsComponent[Index].SaveIndex = Index;

                    Index++;
                    if (--NumberOfSavesFound <= 0)
                        break;
                }
            }
        }
	}
	public void Quit()
    {
        Application.Quit();
    }   
    public void ShowSaveGameScreen()
    {
		if (SavePanelContainer)
			SavePanelContainer.SetActive(true);
	}
    public void HideSaveGameScreen()
    {
		if (SavePanelContainer)
			SavePanelContainer.SetActive(false);
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
    public void OpenMainMenu()
    {
		if (MainMenuSceneIndex >= 0)
			SceneManager.LoadScene(MainMenuSceneIndex);
	}
    public void Resume(GameObject ElementToHide)
    {
        if (ElementToHide)
            ElementToHide.SetActive(false);
    }
}
