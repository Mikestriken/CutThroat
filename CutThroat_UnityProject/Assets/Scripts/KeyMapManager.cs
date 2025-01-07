using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable() => SubscribeToUserInputEvents();
    private void OnDisable() => UnsubscribeFromUserInputEvents();
    
    // ========================================================================
    //                    Assigning To Input Events Handlers
    // ========================================================================
    [SerializeField] private InputReader inputReader;
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
    //                    Handling Menu Button Press Logic
    // ========================================================================
    // ToDo: Figure out how to open main menu while playing during multiplayer...
    // [SerializeField] private GameObject mainMenu;
    [SerializeField] private const string _MENU_SCENE_NAME = "Scenes/Main Menu"; 
    [SerializeField] private const string _GAME_SCENE_NAME = "Scenes/Game"; 


    private void HandleMenuButtonPressedEvent() {
        // mainMenu.SetActive(true);
        SceneManager.LoadScene(_MENU_SCENE_NAME);
        inputReader.SetInputMap(InputReader.InputMaps.IN_UI);

    }

    private void HandleResumeButtonPressedEvent() {
        // mainMenu.SetActive(false);
        SceneManager.LoadScene(_GAME_SCENE_NAME);
        inputReader.SetInputMap(InputReader.InputMaps.IN_GAME);

    }

}
