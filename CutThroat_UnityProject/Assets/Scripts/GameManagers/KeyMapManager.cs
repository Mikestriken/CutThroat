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
    [SerializeField] readonly private SObj_InputReader _inputReader;
    /// <summary>
    /// Subscribes to user input events. Should be called OnEnable.
    /// </summary>
    private void SubscribeToUserInputEvents() {
        _inputReader.MenuEvent += HandleMenuButtonPressedEvent;
        _inputReader.ResumeEvent += HandleResumeButtonPressedEvent;
    }

    /// <summary>
    /// Unsubscribes from user input events. Should be called OnDisable.
    /// </summary>
    private void UnsubscribeFromUserInputEvents() {
        _inputReader.MenuEvent -= HandleMenuButtonPressedEvent;
        _inputReader.ResumeEvent -= HandleResumeButtonPressedEvent;
    }

    // ========================================================================
    //                    Handling Menu Keybind Press Logic
    // ========================================================================
    // ToDo: Figure out how to open main menu while playing during multiplayer...
    // [SerializeField] private GameObject mainMenu;
    private void HandleMenuButtonPressedEvent() {
        // mainMenu.SetActive(true);
        _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_UI);

    }

    private void HandleResumeButtonPressedEvent() {
        // mainMenu.SetActive(false);
        _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);

    }

}
