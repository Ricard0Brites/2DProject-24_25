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
    public GameObject ControlsContainer = null;
    public GameObject DevLevelButton = null;
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

		if (DevLevelButton && !Debug.isDebugBuild)
        {
            Destroy(DevLevelButton);
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
        if (FirstLevelSceneIndex > 0)
            SceneManager.LoadScene(FirstLevelSceneIndex); 
    }
    public void OpenDevLevel()
    {
        if (DevLevelSceneIndex >= 0 && Debug.isDebugBuild)
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
    public void ShowControls()
    {
        if(ControlsContainer)
            ControlsContainer.SetActive(true);
    }
    public void HideControls()
    {
        if(ControlsContainer)
            ControlsContainer.SetActive(false);
	}
}
