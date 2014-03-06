using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float jumpSpeed = 8.0f; 
    bool jumping;

    public float gravity = 20.0f;
    public float moveSpeed = 0.0f;
    public Vector3 moveDirection;

    private float verticalSpeed = 0.0f;
    private Vector3 impulse = Vector3.zero;
    public float damping = 80f;

    float mass_;
    float invMass;
    public float mass
    {
        get
        {
            return mass_;
        }

        set
        {
            mass_ = value;
            invMass = 1.0f / mass_;
        }
    }

    void MoveEvent(float speed)
    {
        moveSpeed = speed;
    }

    public void AddImpuse(Vector3 force)
    {
        impulse += force;
    }

    void Update()
    {
        verticalSpeed -= gravity * Time.deltaTime;

        Vector3 movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0);
        movement *= Time.deltaTime;

        float len = impulse.magnitude;
        if (len > 0.001)
        {
            impulse /= len;
            len -= 0.4f;
            if (len < 0)
            {
                len = 0;
            }

            impulse *= len;
            movement += impulse * Time.deltaTime;     
        }
        
        // Move the controller
        UnityEngine.CharacterController controller = GetComponent<UnityEngine.CharacterController>();
        CollisionFlags flags = controller.Move(movement);
        bool grounded = (flags & CollisionFlags.CollidedBelow) != 0;

        // We are in jump mode but just became grounded
        if (grounded && jumping)
        {
            jumping = false;
            SendMessage("OnLand", SendMessageOptions.DontRequireReceiver);
        } 

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + moveDirection);

    }
}
