using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;


public class UIScript : MonoBehaviour
{
    private const string Main_Audio_Mixer_Volume_Parameter_Name = "ExposedVolumeParameter";
    [SerializeField] private string gameplaySceneName = "Game";
    public AudioMixer mainAudioMixer;

    private Resolution[] validResolutionsForClient;

    [SerializeField] private GameObject Options_Menu_Container;

    private void Start() {
        validResolutionsForClient = Screen.resolutions;
    }

    /// <summary>
    /// Loads current data into the options menu's ui elements
    /// </summary>
    public void LoadOptions() {
        UnityEngine.UI.Slider volumeSlider;
        TMP_Dropdown graphicsQualityDropdown;
        TMP_Dropdown resolutionDropdown;
        UnityEngine.UI.Toggle fullscreenToggle;

        getUIElementsInOptions(out volumeSlider, out graphicsQualityDropdown, out resolutionDropdown, out fullscreenToggle);

        // Update Volume slider
        float currentVolume;
        mainAudioMixer.GetFloat(Main_Audio_Mixer_Volume_Parameter_Name, out currentVolume);
        volumeSlider.value = currentVolume;

        // Update graphics quality dropdown
        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();
        graphicsQualityDropdown.RefreshShownValue();

        // Update Fullscreen checkbox
        fullscreenToggle.isOn = Screen.fullScreen;
        
        // Update Resolutions dropdown
        List<string> resolutionsToDisplay = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < validResolutionsForClient.Length; ++i){
            resolutionsToDisplay.Add(validResolutionsForClient[i].width + " X " + validResolutionsForClient[i].height);
            
            if (Screen.currentResolution.width == validResolutionsForClient[i].width && Screen.currentResolution.height == validResolutionsForClient[i].height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionsToDisplay);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionsToDisplay = null;
    }

    /// <summary>
    /// Iterates through the children of the Options_Menu_Container GameObject and returns references to relevant UI elements
    /// </summary>
    /// <param name="volumeSlider">Variable to store Reference for volumeSlider in Options Menu</param>
    /// <param name="graphicsQualityDropdown">Variable to store Reference for graphicsQualityDropdown in Options Menu</param>
    /// <param name="resolutionDropdown">Variable to store Reference for resolutionDropdown in Options Menu</param>
    /// <param name="fullscreenToggle">Variable to store Reference for fullscreenToggle in Options Menu</param>
    private void getUIElementsInOptions(out UnityEngine.UI.Slider volumeSlider, out TMP_Dropdown graphicsQualityDropdown, out TMP_Dropdown resolutionDropdown, out UnityEngine.UI.Toggle fullscreenToggle) {
        
        volumeSlider = null;
        graphicsQualityDropdown = null;
        resolutionDropdown = null;
        fullscreenToggle = null;

        Transform Options_Menu_Container_Transform = Options_Menu_Container.transform;

        for (int i = 0; i < Options_Menu_Container_Transform.childCount; i++) {
            switch (Options_Menu_Container_Transform.GetChild(i).gameObject.name)
            {
                case "Volume Slider":
                    volumeSlider = Options_Menu_Container_Transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Slider>();
                    break;

                case "Graphics":
                    graphicsQualityDropdown = Options_Menu_Container_Transform.GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Dropdown>();
                    break;

                case "Screen Settings":
                    resolutionDropdown = Options_Menu_Container_Transform.GetChild(i).GetChild(0).gameObject.GetComponent<TMP_Dropdown>();
                    fullscreenToggle = Options_Menu_Container_Transform.GetChild(i).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Toggle>();
                    break;
            }
        }
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();        
    }

    public void QuitGame() => Application.Quit();

    // Todo: Update settings on start to current value
    public void SetVolume(float newVolumeValue) => 
        mainAudioMixer.SetFloat(Main_Audio_Mixer_Volume_Parameter_Name, newVolumeValue); 

    public void SetQuality(int newQualityIndex) => 
        QualitySettings.SetQualityLevel(newQualityIndex); 

    public void SetFullscreen(bool setFullscreen) => 
        Screen.fullScreen = setFullscreen; 

    public void SetResolution(int newResolutionIndex) {
        Resolution newResolution = validResolutionsForClient[newResolutionIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }
}
