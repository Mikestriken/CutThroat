using UnityEngine;

/// <summary>
/// Handles movement physics for player
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ClientKeybindHandler))]
public class ClientActorPhysics : MonoBehaviour
{
    // =================================================================================
    //                              MonoBehavior Methods
    // =================================================================================

    // I was told to update rigidBodies in fixed update, source: https://www.youtube.com/watch?v=u42aWzAIAqg
    private void FixedUpdate() => updateMovement();

    // =================================================================================
    //                            Client Actor Physics Logic
    // =================================================================================
    [SerializeField] readonly private Rigidbody2D _actorRigidBody;
    [SerializeField] readonly private ClientKeybindHandler _clientInputs;

    /// <summary>
    /// Updates movement physics of actor using currently pressed keys
    /// </summary>
    void updateMovement() {
        const float actorMoveSpeed = 3f;
        
        Vector2 actorMoveDirection = _clientInputs.actorMoveDirection;

        switch (actorMoveDirection)
        {
            case Vector2 v when (v == Vector2.left || v == Vector2.right):
                _actorRigidBody.linearVelocity = actorMoveDirection * actorMoveSpeed + Vector2.up * _actorRigidBody.linearVelocity.y;
                break;

            case Vector2 v when v == Vector2.up:
                _actorRigidBody.linearVelocity = actorMoveDirection * actorMoveSpeed;
                break;

            case Vector2 v when v == Vector2.down:

                break;
        }
    }
}
