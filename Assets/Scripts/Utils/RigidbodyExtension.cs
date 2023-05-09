using UnityEngine;
using System.Collections;

public static class RigidbodyExtension
{

    static Vector3 velocity;
    static Vector3 angularVelocity;

    public static void Pause(this Rigidbody rb)
    {
        velocity = rb.velocity;
        angularVelocity = rb.angularVelocity;
        rb.isKinematic = true;

    }
    public static void UnPause(this Rigidbody rb)
    {
        rb.isKinematic = false;
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
    }
}