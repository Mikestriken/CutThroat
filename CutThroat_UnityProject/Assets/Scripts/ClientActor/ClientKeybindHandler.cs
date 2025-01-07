using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles keybind input states for client
/// </summary>
// ToDO: Rename to ClientActorKeypressEventsHandler
public class ClientKeybindHandler : MonoBehaviour
{
    // =================================================================================
    //                              MonoBehavior Methods
    // =================================================================================
    private void OnEnable() => SubscribeToUserInputEventsForActor();
    private void OnDisable() => UnsubscribeFromUserInputEventsForActor();

    // =================================================================================
    //                              Keypress Events Logic
    // =================================================================================
    // * Stores button states
    [SerializeField] readonly private SObj_InputReader _inputReader;
    public Vector2 actorMoveDirection {get; private set;} = Vector2.zero;
    public bool dashButtonPressed {get; private set;} = false;
    public bool jumpButtonPressed {get; private set;} = false;
    public bool attackButtonPressed {get; private set;} = false;
    public bool blockButtonPressed {get; private set;} = false;


    /// <summary>
    /// Subscribes keypress event handlers to user input events when actor is created / enabled
    /// </summary>
    private void SubscribeToUserInputEventsForActor() {
        _inputReader.MoveEvent += UpdateMoveDirection;
        _inputReader.DashEvent += UpdateDashButtonState;
        _inputReader.JumpEvent += UpdateJumpButtonState;
        _inputReader.AttackEvent += UpdateAttackButtonState;
        _inputReader.BlockEvent += UpdateBlockButtonState;
    }


    /// <summary>
    /// Unsubscribes keypress event handlers from user input events when Actor is destroyed / disabled
    /// </summary>
    private void UnsubscribeFromUserInputEventsForActor() {
        _inputReader.MoveEvent -= UpdateMoveDirection;
        _inputReader.DashEvent -= UpdateDashButtonState;
        _inputReader.JumpEvent -= UpdateJumpButtonState;
        _inputReader.AttackEvent -= UpdateAttackButtonState;
        _inputReader.BlockEvent -= UpdateBlockButtonState;
    }

    // * Updates button states
    private void UpdateMoveDirection(Vector2 newDirection) => actorMoveDirection = newDirection;
    private void UpdateDashButtonState(bool buttonIsPressed) => dashButtonPressed = buttonIsPressed;
    private void UpdateJumpButtonState(bool buttonIsPressed) => jumpButtonPressed = buttonIsPressed;
    private void UpdateAttackButtonState(bool buttonIsPressed) => attackButtonPressed = buttonIsPressed;
    private void UpdateBlockButtonState(bool buttonIsPressed) => blockButtonPressed = buttonIsPressed;
}
