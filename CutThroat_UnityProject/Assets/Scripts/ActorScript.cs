using UnityEngine;
using UnityEngine.InputSystem;

public class ActorScript : MonoBehaviour
{
    public Rigidbody2D actorRigidBody;
    public ActorKeybinds actorKeybinds;

    private Vector2 actorMoveDirection = Vector2.zero;

    private InputAction actorMoveKeys;
    private InputAction actorAttackKeys;

    private void Awake() {
        actorKeybinds = new ActorKeybinds();
    }
    
    private void OnEnable() {
        actorMoveKeys = actorKeybinds.Player.Move;
        actorMoveKeys.Enable();

        actorAttackKeys = actorKeybinds.Player.Attack;
        actorAttackKeys.Enable();
        actorAttackKeys.performed += Attack;
    }

    private void OnDisable() {
        actorMoveKeys.Disable();
        
        actorAttackKeys.performed -= Attack;
        actorAttackKeys.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actorMoveDirection = actorMoveKeys.ReadValue<Vector2>();

    }

    private void FixedUpdate() {
        // I was told to update rigidBodies in fixed update, source: https://www.youtube.com/watch?v=u42aWzAIAqg
        updateMovement();
        
    }

    void updateMovement() {
        const float actorMoveSpeed = 3f;
        
        switch (actorMoveDirection)
        {
            case Vector2 v when (v == Vector2.left || v == Vector2.right):
                actorRigidBody.linearVelocity = actorMoveDirection * actorMoveSpeed + Vector2.up * actorRigidBody.linearVelocity.y;
                break;

            case Vector2 v when v == Vector2.up:
                actorRigidBody.linearVelocity = actorMoveDirection * actorMoveSpeed;
                break;

            case Vector2 v when v == Vector2.down:

                break;
        }
    }

    private void Attack(InputAction.CallbackContext context) {
        Debug.Log("We Fired!");
    }
}
