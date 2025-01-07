using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager that handles switching between different keybind maps when appropriate.
/// </summary>
// Todo: Create scene switcher manager use dependency injection to make it require a KeyMapManager
    // Note: Only reason keybinds work is because InputReader initializes InputMap to InputMaps.IN_GAME
// ToDo: Create pause menu
// ToDo: Disable ResumeEvent handling if game not started yet
public class KeyMapManager : MonoBehaviour
{
    private void OnEnable() => SubscribeToUserInputEvents();
    private void OnDisable() => UnsubscribeFromUserInputEvents();
    
    // ========================================================================
    //                    Assigning To Input Events Handlers
    // ========================================================================
    [SerializeField] private SObj_InputReader inputReader;
    /// <summary>
    /// Subscribes to user input events. Should be called OnEnable.
    /// </summary>
    private void SubscribeToUserInputEvents() {
        inputReader.MenuEvent += HandleMenuButtonPressedEvent;
        inputReader.ResumeEvent += HandleResumeButtonPressedEvent;
    }

    /// <summary>
    /// Unsubscribes from user input events. Should be called OnDisable.
    /// </summary>
    private void UnsubscribeFromUserInputEvents() {
        inputReader.MenuEvent -= HandleMenuButtonPressedEvent;
        inputReader.ResumeEvent -= HandleResumeButtonPressedEvent;
    }

    // ========================================================================
    //                    Handling Menu Keybind Press Logic
    // ========================================================================
    // ToDo: Figure out how to open main menu while playing during multiplayer...
    // [SerializeField] private GameObject mainMenu;
    [SerializeField] private const string _MENU_SCENE_NAME = "Scenes/Main Menu"; 
    [SerializeField] private const string _GAME_SCENE_NAME = "Scenes/Game"; 


    private void HandleMenuButtonPressedEvent() {
        // mainMenu.SetActive(true);
        SceneManager.LoadScene(_MENU_SCENE_NAME);
        inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_UI);

    }

    private void HandleResumeButtonPressedEvent() {
        // mainMenu.SetActive(false);
        SceneManager.LoadScene(_GAME_SCENE_NAME);
        inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);

    }

}
