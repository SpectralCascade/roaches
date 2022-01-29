using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    public Rigidbody rigidBody;
    public float distanceToShowRealHandPosition = 0.05f;
    public Renderer nonPhysicalHand;
    private Collider[] colliders;

    private void Start() {
        colliders = GetComponentsInChildren<Collider>();
    }

    private void EnableCollidersDelay() {
        for (int i = 0, counti = colliders.Length; i < counti; i++) {
            colliders[i].enabled = true;
        }
    }

    public void EnableHandCollider() {
        Invoke("EnableCollidersDelay", 0.3f);
    }

    public void DisableHandCollider() {
        for (int i = 0, counti = colliders.Length; i < counti; i++) {
            colliders[i].enabled = false;
        }
    }

    private void Update() {
        nonPhysicalHand.enabled = Vector3.Distance(transform.position, target.position) > distanceToShowRealHandPosition;
    }

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
