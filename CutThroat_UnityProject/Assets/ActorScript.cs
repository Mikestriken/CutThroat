using UnityEngine;

public class ActorScript : MonoBehaviour
{
    public Rigidbody2D actorRigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        const float actorMoveSpeed = 1f;

        // switch (switch_on)
        // {
            
        //     default:
        // }
        if (Input.GetKey(KeyCode.RightArrow))
            actorRigidBody.linearVelocity = Vector2.right * actorMoveSpeed;
        
    }
}
