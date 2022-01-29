using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    public Rigidbody rigidBody;

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidBody.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angle, out Vector3 rotAxis);


        Vector3 rotDiff = angle * rotAxis;

        rigidBody.angularVelocity = (rotDiff * Mathf.Deg2Rad) / Time.fixedDeltaTime;
    }
}
