using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just a jump pad, sets an object's velocity on trigger, can also be done on collision but isn't quite right.
/// @Author 256
/// </summary>

public class JumpPad : MonoBehaviour {

    public Vector3 PushDirection = Vector3.up;
    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
                rb.AddForce(PushDirection, ForceMode.VelocityChange);
            //for player controllers with no Rb but have a knockback velocity.
            //PlayerController player = other.gameObject.GetComponent<PlayerController>();
            //if (player != null)
            //    player.knockBackVel = PushDirection;
        }
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log("player collider operating");
        }

    }*/
}
