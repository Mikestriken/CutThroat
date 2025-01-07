using UnityEngine;
using System.Collections;

/// <summary>
/// Handles keybind input states for client
/// </summary>
public class ClientKeybindHandler : MonoBehaviour
{
    // =================================================================================
    //                              MonoBehavior Methods
    // =================================================================================
    private void OnEnable() => SubscribeToUserInputEvents();
    private void OnDisable() => UnsubscribeFromUserInputEvents();

    private void Awake() => StartCoroutine(RetrieveInGameMenuDependency());

    // =================================================================================
    //                              Keypress Events Logic
    // =================================================================================
    // * Stores button states
    [SerializeField] private SObj_InputReader _inputReader;
    public Vector2 actorMoveDirection {get; private set;} = Vector2.zero;
    public bool dashButtonPressed {get; private set;} = false;
    public bool jumpButtonPressed {get; private set;} = false;
    public bool attackButtonPressed {get; private set;} = false;
    public bool blockButtonPressed {get; private set;} = false;


    /// <summary>
    /// Subscribes keypress event handlers to user input events when actor is created / enabled
    /// </summary>
    private void SubscribeToUserInputEvents() {
        _inputReader.MoveEvent += UpdateMoveDirection;
        _inputReader.DashEvent += UpdateDashButtonState;
        _inputReader.JumpEvent += UpdateJumpButtonState;
        _inputReader.AttackEvent += UpdateAttackButtonState;
        _inputReader.BlockEvent += UpdateBlockButtonState;
        _inputReader.MenuEvent += HandleMenuButtonPressedEvent;
        _inputReader.ResumeEvent += HandleResumeButtonPressedEvent;
    }


    /// <summary>
    /// Unsubscribes keypress event handlers from user input events when Actor is destroyed / disabled
    /// </summary>
    private void UnsubscribeFromUserInputEvents() {
        _inputReader.MoveEvent -= UpdateMoveDirection;
        _inputReader.DashEvent -= UpdateDashButtonState;
        _inputReader.JumpEvent -= UpdateJumpButtonState;
        _inputReader.AttackEvent -= UpdateAttackButtonState;
        _inputReader.BlockEvent -= UpdateBlockButtonState;
        _inputReader.MenuEvent -= HandleMenuButtonPressedEvent;
        _inputReader.ResumeEvent -= HandleResumeButtonPressedEvent;
    }

    // * Updates button states
    private void UpdateMoveDirection(Vector2 newDirection) => actorMoveDirection = newDirection;
    private void UpdateDashButtonState(bool buttonIsPressed) => dashButtonPressed = buttonIsPressed;
    private void UpdateJumpButtonState(bool buttonIsPressed) => jumpButtonPressed = buttonIsPressed;
    private void UpdateAttackButtonState(bool buttonIsPressed) => attackButtonPressed = buttonIsPressed;
    private void UpdateBlockButtonState(bool buttonIsPressed) => blockButtonPressed = buttonIsPressed;

    // * UI Keypress Handlers
    [SerializeField] private GameObject _inGameMenu;
    /// <summary>
    /// Retrieves a reference to the in game menu and sets it to the _inGameMenu property.
    /// </summary>
    // ToDo: Change this to a dependency injection in the NetworkManager (maybe inherit it and make a custom class?)
    private IEnumerator RetrieveInGameMenuDependency() {
        const string IN_GAME_MENU_GAMEOBJECT_NAME = "Main Menu Panel";
        const string CANVAS_GAMEOBJECT_NAME = "Canvas";
        
        yield return new WaitForSecondsRealtime(0.5f); // Wait until new scene loaded

        // Note: IN_GAME_MENU is disabled initially so GameObject.Find will not work.
        yield return new WaitUntil(() => {
            Transform canvasTransform = GameObject.Find(CANVAS_GAMEOBJECT_NAME).transform;

            for(int i = 0; i < canvasTransform.childCount; ++i) {
                bool foundMainMenuPanel = canvasTransform.GetChild(i).gameObject.name == IN_GAME_MENU_GAMEOBJECT_NAME;
                
                if (foundMainMenuPanel) return true;
            }

            return false;
        });

        Transform canvasTransform = GameObject.Find(CANVAS_GAMEOBJECT_NAME).transform;
        for(int i = 0; i < canvasTransform.childCount; ++i)
            if(canvasTransform.GetChild(i).gameObject.name == IN_GAME_MENU_GAMEOBJECT_NAME)
                _inGameMenu = canvasTransform.GetChild(i).gameObject;
        ;
    }

    private void HandleMenuButtonPressedEvent() {
        _inGameMenu.SetActive(true);
        _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_UI);

    }

    private void HandleResumeButtonPressedEvent() {
        _inGameMenu.SetActive(false);
        _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);

    }
}
