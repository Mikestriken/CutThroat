using UnityEngine;
using UnityEngine.InputSystem;

public class ActorScript : MonoBehaviour
{
    // ====================================================================
    //                          Actor Physics Logic
    // ====================================================================
    public Rigidbody2D actorRigidBody;

    private void FixedUpdate() {
        // I was told to update rigidBodies in fixed update, source: https://www.youtube.com/watch?v=u42aWzAIAqg
        updateMovement();
    }

    void updateMovement() {
        const float actorMoveSpeed = 3f;
        
        switch (_actorMoveDirection)
        {
            case Vector2 v when (v == Vector2.left || v == Vector2.right):
                actorRigidBody.linearVelocity = _actorMoveDirection * actorMoveSpeed + Vector2.up * actorRigidBody.linearVelocity.y;
                break;

            case Vector2 v when v == Vector2.up:
                actorRigidBody.linearVelocity = _actorMoveDirection * actorMoveSpeed;
                break;

            case Vector2 v when v == Vector2.down:

                break;
        }
    }

    // ====================================================================
    //                        User Input Logic
    // ====================================================================
    // * Stores button states
    [SerializeField] private SObj_InputReader inputReader;
    private Vector2 _actorMoveDirection = Vector2.zero;
    private bool _dashButtonPressed = false;
    private bool _jumpButtonPressed = false;
    private bool _attackButtonPressed = false;
    private bool _blockButtonPressed = false;

    /// <summary>
    /// Subscribes to user input events when actor is created / enabled
    /// </summary>
    private void OnEnable() {
        inputReader.MoveEvent += UpdateMoveDirection;
        inputReader.DashEvent += UpdateDashButtonState;
        inputReader.JumpEvent += UpdateJumpButtonState;
        inputReader.AttackEvent += UpdateAttackButtonState;
        inputReader.BlockEvent += UpdateBlockButtonState;
    }


    /// <summary>
    /// Unsubscribes from user input events when Actor is destroyed / disabled
    /// </summary>
    private void OnDisable() {
        inputReader.MoveEvent -= UpdateMoveDirection;
        inputReader.DashEvent -= UpdateDashButtonState;
        inputReader.JumpEvent -= UpdateJumpButtonState;
        inputReader.AttackEvent -= UpdateAttackButtonState;
        inputReader.BlockEvent -= UpdateBlockButtonState;
    }

    // * Updates button states
    private void UpdateMoveDirection(Vector2 newDirection) => _actorMoveDirection = newDirection;
    private void UpdateDashButtonState(bool buttonIsPressed) => _dashButtonPressed = buttonIsPressed;
    private void UpdateJumpButtonState(bool buttonIsPressed) => _jumpButtonPressed = buttonIsPressed;
    private void UpdateAttackButtonState(bool buttonIsPressed) => _attackButtonPressed = buttonIsPressed;
    private void UpdateBlockButtonState(bool buttonIsPressed) => _blockButtonPressed = buttonIsPressed;
}
