using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsLogic : MonoBehaviour
{
    public int FirstLevelSceneIndex = -1, DevLevelSceneIndex = -1;
    public int MainMenuSceneIndex = -1;

    public GameObject SavePanelContainer = null;
    public GameObject SaveButtonsContainer = null;
    public GameObject SettingsContainer = null;
    public Slider VolumeSlider = null;
    public Dropdown GraphicQuality = null;
    public Dropdown WindowMode = null;
    public AudioMixer Mixer = null;

	private void Start()
	{
        // Hide the Save Panel (done here for convenience)
        if (SavePanelContainer)
            SavePanelContainer.SetActive(false);
        if (SettingsContainer)
            SettingsContainer.SetActive(false);
        if (GraphicQuality)
            GraphicQuality.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        if (WindowMode)
            WindowMode.SetValueWithoutNotify(Screen.fullScreen ? 1 : 0);
        if (Mixer)
        {
            float CurrentVolume;
            Mixer.GetFloat("MasterVolume", out CurrentVolume);
    		if (VolumeSlider)
                VolumeSlider.SetValueWithoutNotify(CurrentVolume);
        }
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
    public void HideSettingsScreen()
    {
        if (SettingsContainer)
            SettingsContainer.SetActive(false);
    }
    public void StartNewGame()
    {
        Debug.Log("Starting New Game");
        if (FirstLevelSceneIndex > 0)
            SceneManager.LoadScene(FirstLevelSceneIndex); 
    }
    public void OpenDevLevel()
    {
        if (DevLevelSceneIndex >= 0)
            SceneManager.LoadScene(DevLevelSceneIndex);
    }
    public void OpenSettings()
    {
        if (SettingsContainer)
            SettingsContainer.SetActive(true);
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

		Time.timeScale = 1; 
	}
    public void ChangeWindowMode(int NewWindowMode)
    {
        Screen.fullScreen = NewWindowMode > 0;
    }
    public void ChangeGraphicQuality(int NewQuality)
    {
        QualitySettings.SetQualityLevel(NewQuality);
    }
    public void ChangeMasterVolume(float NewVolume)
    {
        if(Mixer)
            Mixer.SetFloat("MasterVolume", NewVolume);
    }
}
