using UnityEngine;
using UnityEngine.InputSystem;

public class ActorScript : MonoBehaviour
{
    public Rigidbody2D actorRigidBody;
    public ActorKeybinds actorKeybinds;

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
    }

    private void OnDisable() {
        actorMoveKeys.Disable();
        actorAttackKeys.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();

    }

    void updateMovement() {
        const float actorMoveSpeed = 3f;
        
        Vector2 moveDirection = Vector2.zero;
        moveDirection = actorMoveKeys.ReadValue<Vector2>();

        switch (moveDirection)
        {
            case Vector2 v when (v == Vector2.left || v == Vector2.right):
                actorRigidBody.linearVelocity = moveDirection * actorMoveSpeed;
                break;

            case Vector2 v when v == Vector2.up:
                actorRigidBody.linearVelocity = moveDirection * actorMoveSpeed;
                break;

            case Vector2 v when v == Vector2.down:

                break;
        }
    }
}
