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
using System;

/// <summary>
/// Mega script to handle all UI button logic and data
/// </summary>
// ToDo: We should really split this script up.... Mega scripts are bad practice... Ask Gray for permission to do this.
public class UIScript : MonoBehaviour
{
    private void Start() => RetrieveValidResolutionsForClient();

    // =================================================================================
    //                              Options Menu Logic
    // =================================================================================
    [SerializeField] private AudioMixer _mainAudioMixer;
    private const string _MAIN_AUDIO_MIXER_VOLUME_PARAMETER_NAME = "ExposedVolumeParameter";
    [SerializeField] private GameObject _optionsMenuContainer;

    // Tuple to contain resolution.width, resolution.height
    private List<Tuple<int, int>> _validResolutionsForClient;
    /// <summary>
    /// Retrieves and stores a list of valid resolutions for the client in the _validResolutionsForClient property during Awake() event.
    /// Also filters retrieved list of resolutions to just a single refresh rate
    /// </summary>
    private void RetrieveValidResolutionsForClient() {
        Resolution[] validResolutionsForClient = Screen.resolutions;

        _validResolutionsForClient = new List<Tuple<int, int>>();

        // Note: In unity editor only only current refresh rate resolutions show up in Screen.resolutions.
        //       But builds contain ALL refresh rate x screen resolution combinations.
        double currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        foreach (Resolution resolution in validResolutionsForClient) {
            if (resolution.refreshRateRatio.value == currentRefreshRate)
                _validResolutionsForClient.Add(Tuple.Create(resolution.width, resolution.height));
        }
    }

    /// <summary>
    /// Should be called to open the options menu.
    /// Updates settings menu with currently set settings then sets it active.
    /// </summary>
    public void LoadOptions() {
        UnityEngine.UI.Slider volumeSlider;
        TMP_Dropdown graphicsQualityDropdown;
        TMP_Dropdown resolutionDropdown;
        UnityEngine.UI.Toggle fullscreenToggle;

        getUIElementsInOptions(out volumeSlider, out graphicsQualityDropdown, out resolutionDropdown, out fullscreenToggle);

        // Update Volume slider
        float currentVolume;
        _mainAudioMixer.GetFloat(_MAIN_AUDIO_MIXER_VOLUME_PARAMETER_NAME, out currentVolume);
        volumeSlider.value = currentVolume;

        // Update graphics quality dropdown
        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();
        graphicsQualityDropdown.RefreshShownValue();

        // Update Fullscreen checkbox
        fullscreenToggle.isOn = Screen.fullScreen;
        
        // Update Resolutions dropdown
        List<string> resolutionsToDisplay = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _validResolutionsForClient.Count; ++i){
            resolutionsToDisplay.Add(_validResolutionsForClient[i].Item1 + " X " + _validResolutionsForClient[i].Item2);
            
            if (Screen.currentResolution.width == _validResolutionsForClient[i].Item1 && Screen.currentResolution.height == _validResolutionsForClient[i].Item2)
                currentResolutionIndex = i;
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionsToDisplay);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionsToDisplay = null;

        // After finished updating Options Menu UI data.
        _optionsMenuContainer.SetActive(true);
    }

    /// <summary>
    /// Iterates through the children of the Options_Menu_Container GameObject and returns references to relevant UI elements
    /// </summary>
    /// <param name="volumeSlider">Variable to store Reference for volumeSlider in Options Menu</param>
    /// <param name="graphicsQualityDropdown">Variable to store Reference for graphicsQualityDropdown in Options Menu</param>
    /// <param name="resolutionDropdown">Variable to store Reference for resolutionDropdown in Options Menu</param>
    /// <param name="fullscreenToggle">Variable to store Reference for fullscreenToggle in Options Menu</param>
    private void getUIElementsInOptions(out UnityEngine.UI.Slider volumeSlider, 
                                        out TMP_Dropdown graphicsQualityDropdown, 
                                        out TMP_Dropdown resolutionDropdown, 
                                        out UnityEngine.UI.Toggle fullscreenToggle) {
        
        volumeSlider = null;
        graphicsQualityDropdown = null;
        resolutionDropdown = null;
        fullscreenToggle = null;

        Transform Options_Menu_Body_Container_Transform = _optionsMenuContainer.transform.GetChild(1);

        for (int i = 0; i < Options_Menu_Body_Container_Transform.childCount; i++) {
            switch (Options_Menu_Body_Container_Transform.GetChild(i).gameObject.name)
            {
                case "Volume Slider":
                    volumeSlider = Options_Menu_Body_Container_Transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Slider>();
                    break;

                case "Graphics":
                    graphicsQualityDropdown = Options_Menu_Body_Container_Transform.GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Dropdown>();
                    break;

                case "Screen Settings":
                    resolutionDropdown = Options_Menu_Body_Container_Transform.GetChild(i).GetChild(0).gameObject.GetComponent<TMP_Dropdown>();
                    fullscreenToggle = Options_Menu_Body_Container_Transform.GetChild(i).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Toggle>();
                    break;
            }
        }
    }

    public void SetVolume(float newVolumeValue) => 
        _mainAudioMixer.SetFloat(_MAIN_AUDIO_MIXER_VOLUME_PARAMETER_NAME, newVolumeValue); 

    public void SetQuality(int newQualityIndex) => 
        QualitySettings.SetQualityLevel(newQualityIndex); 

    public void SetFullscreen(bool setFullscreen) => 
        Screen.fullScreen = setFullscreen; 

    public void SetResolution(int newResolutionIndex) {
        // Tuple ints are Width, Height
        Tuple<int, int> newResolution = _validResolutionsForClient[newResolutionIndex];

        Screen.SetResolution(newResolution.Item1, newResolution.Item2, Screen.fullScreen);
    }

    // =================================================================================
    //                              Main Menu Logic
    // =================================================================================
    [SerializeField] private const string _gameplaySceneName = "Game";

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(_gameplaySceneName, LoadSceneMode.Single);
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene(_gameplaySceneName, LoadSceneMode.Single);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();        
    }

    public void QuitGame() => Application.Quit();
}
