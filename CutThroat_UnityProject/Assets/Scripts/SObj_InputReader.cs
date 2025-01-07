using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles keybind events as they initially appear. 
/// Invokes custom events after each keybind's context parameter has been resolved as best as possible.
/// </summary>
// ToDo: Figure out why Actors translate right when switching from InGame map to InUI map
[CreateAssetMenu(menuName = "InputReader")]
public class SObj_InputReader : ScriptableObject, InputMap.IInGameActions, InputMap.IInUIActions 
{
    private InputMap _inputMap;

    private void OnEnable() {
        if (_inputMap == null) {
            _inputMap = new InputMap();

            // unsubscribe _inputMap's events handlers 
            // subscribe them to this class's implementation of them instead.
            _inputMap.InGame.SetCallbacks(this);
            _inputMap.InUI.SetCallbacks(this);

            SetInputMap(InputMaps.IN_GAME);
        }
    }

    /// <summary>
    /// If the InputReader instance is ever destroyed, assert that the current map should be disabled
    /// </summary>
    ~SObj_InputReader() {
        Debug.Assert((!_inputMap?.InGame.enabled) ?? true, "ERR: InGame Map enabled on destruction");
        Debug.Assert((!_inputMap?.InUI.enabled) ?? true, "ERR: InUI Map enabled on destruction");
    }

    private void OnDisable() {
        SetInputMap(InputMaps.NONE);
    }

    // =========================================================================
    //                                Helper Methods
    // =========================================================================
    // Set Current Keybind mapping context
    public enum InputMaps
    {
        IN_GAME,
        IN_UI,
        NONE
    }

    public InputMaps currentMap {get; private set;}

    /// <summary>
    /// Public method to control the current keymapping state for the game
    /// </summary>
    /// <param name="newMap">Enum for which keymap to switch to</param>
    public void SetInputMap(InputMaps newMap) {
        switch (newMap)
        {
            case InputMaps.IN_GAME:
                _inputMap.InGame.Enable();
                _inputMap.InUI.Disable();
                currentMap = InputMaps.IN_GAME;
                break;

            case InputMaps.IN_UI:
                _inputMap.InUI.Enable();
                _inputMap.InGame.Disable();
                currentMap = InputMaps.IN_UI;
                break;

            case InputMaps.NONE:
                _inputMap.InUI.Disable();
                _inputMap.InGame.Disable();
                currentMap = InputMaps.NONE;
                break;
        }
    }

    // =========================================================================
    //                                Custom Events 
    // =========================================================================
    // InGame Events
    public event System.Action<Vector2> MoveEvent;
    public event System.Action<bool> DashEvent;
    public event System.Action<bool> JumpEvent;
    public event System.Action<bool> AttackEvent;
    public event System.Action<bool> BlockEvent;
    public event System.Action MenuEvent;

    // InUI Events
    public event System.Action ResumeEvent;

    // =========================================================================
    //          InputMap Interface Implementations for Event Handlers
    // =========================================================================
    // * Purpose: resolve the context parameter in this abstraction before invoking our own Event after it has been resolved.

    // InGame Events
    void InputMap.IInGameActions.OnMove(InputAction.CallbackContext context) => 
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    void InputMap.IInGameActions.OnDash(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
            DashEvent?.Invoke(true);

        else if (context.phase == InputActionPhase.Canceled)
            DashEvent?.Invoke(false);
    }

    void InputMap.IInGameActions.OnJump(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent?.Invoke(true);

        else if (context.phase == InputActionPhase.Canceled)
            JumpEvent?.Invoke(false);
    }

    void InputMap.IInGameActions.OnAttack(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
            AttackEvent?.Invoke(true);

        else if (context.phase == InputActionPhase.Canceled)
            AttackEvent?.Invoke(false);
    }

    void InputMap.IInGameActions.OnBlock(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
            BlockEvent?.Invoke(true);

        else if (context.phase == InputActionPhase.Canceled)
            BlockEvent?.Invoke(false);
    }

    void InputMap.IInGameActions.OnMenu(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
            MenuEvent?.Invoke();
    }

    // InUI Events
    void InputMap.IInUIActions.OnResume(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
            ResumeEvent?.Invoke();
    }
}