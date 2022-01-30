using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using OpenGET;

public class RoachController : MonoBehaviour
{

    private float scale = 1.0f;

    public Transform front;
    public Transform back;

    // Speed in metres per second
    public float speed = 1.0f;

    // Turn speed in degrees per second
    public float turnSpeed = 45f;

    // The force used to stick to surfaces
    public float stickiness = 1.0f;

    public float tiltForce = 1.0f;

    public float jumpForce = 1.0f;

    // Target points on surface
    public Vector3 targetPointFront = Vector3.zero;
    public Vector3 targetPointBack = Vector3.zero;

    [Range(0, 1)]
    public float frontRayLength = 2f;

    [Range(0, 1)]
    public float belowRayLength = 1f;

    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    private GameObject roachModel;

    [SerializeField]
    private GameObject splatDecalPrefab;

    // Tracks movement from input
    private Vector2 movement;

    // Raycast results for front forward, front down and back down raycasts
    private bool colliderInFront = false;
    private bool colliderBelowFore = false;
    private bool colliderBelowAft = false;

    public float killSpeed = 3.0f;

    public void OnMove(InputValue value) {
        movement = value.Get<Vector2>();
        //Log.Info("Moving {0}", movement);
    }

    bool flip = false;

    public void OnJump(InputValue value) {
        // Check if roach is wrong way up
        bool upsideDown = Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 0.4f * scale) ||
            Physics.Raycast(transform.position, transform.right, out hit, 0.4f * scale) ||
            Physics.Raycast(transform.position, transform.right * -1f, out hit, 0.4f * scale) ||
            Physics.Raycast(transform.position, transform.forward, out hit, 0.4f * scale) ||
            Physics.Raycast(transform.position, transform.forward * -1f, out hit, 0.4f * scale);

        if (upsideDown && body.useGravity) {
            // Try flipping
            // Log.Info("UPSIDE DOWN! Flipping over :)");
            body.AddForceAtPosition(transform.up * -tiltForce * 9f * scale, front.position, ForceMode.Impulse);
        } else if (!body.useGravity) {
            // Jump!
            //Log.Info("JUMP!");
            body.AddForce(transform.up * jumpForce * scale, ForceMode.Impulse);
        }
    }

    private void Start() {
        scale = transform.localScale.z;
        //frontRayLength *= scale;
        //belowRayLength *= scale;
        stickiness *= body.mass;
        tiltForce *= body.mass;
    }

    public void OnRoachHit(Vector3 velocity) {
        if (velocity.magnitude > killSpeed) {
            //enabled = false;
            //roachModel.SetActive(false);
            if (Physics.Raycast(body.position, velocity.normalized, out RaycastHit hit)) {
                GameObject splat = Instantiate(splatDecalPrefab);
                splat.transform.position = hit.point + hit.normal * 0.001f;
                splat.transform.rotation = Quaternion.LookRotation(hit.normal);
                Log.Info("ROACH KILLED");
                AudioController.Play("SFX_Squish");
            }
        }
    }

    private void Update()
    {
        colliderInFront = Physics.Raycast(front.position, transform.forward, out RaycastHit frontHit, frontRayLength);
        colliderBelowFore = Physics.Raycast(front.position, transform.up * -1f, out RaycastHit belowForeHit, belowRayLength);
        colliderBelowAft = Physics.Raycast(back.position, transform.up * -1f, out RaycastHit belowAftHit, belowRayLength);
        //bool rayBelow = Physics.Raycast(transform.position, transform.up * -1f, out RaycastHit belowAftHit, belowRayLength);

        //Log.Info("Collider in front: {0}, collider down fore: {1}, collider down aft: {2}", colliderInFront, colliderBelowFore, colliderBelowAft);

        // Default to no gravity
        body.useGravity = false;
        if (flip || (colliderInFront && colliderBelowFore && frontHit.collider != belowAftHit.collider)) {
            // Obstacle in front of roach, try and climb it
            body.AddForceAtPosition(transform.up * tiltForce, front.position);
            body.AddForceAtPosition(transform.up * -1f * tiltForce, back.position);
            body.AddForce(frontHit.normal * -1f * stickiness);
        } else if ((!colliderBelowFore ^ !colliderBelowAft) && !colliderInFront) {
            // Must be coming over a edge! Tilt over it
            body.AddForceAtPosition(transform.up * -1f * tiltForce * 0.5f, front.position);
            body.AddForceAtPosition(transform.up * tiltForce * 0.5f, back.position);
        } else if (colliderBelowFore && colliderBelowAft && !colliderInFront) {
            // Must be grounded, move normally
            body.AddForceAtPosition(belowForeHit.normal * -1f * stickiness, front.position);
            body.AddForceAtPosition(belowAftHit.normal * -1f * stickiness, back.position);
        } else if (!colliderInFront && !colliderBelowFore && !colliderBelowAft) {
            // Must be in free-fall!
            body.useGravity = true;
            //body.MoveRotation(transform.rotation * Quaternion.LookRotation(Vector3.up));
        }

        // Now apply movement forces
        body.MovePosition(transform.position + (movement.y * transform.forward * speed * Time.deltaTime));
        body.MoveRotation(transform.rotation * Quaternion.AngleAxis(movement.x * turnSpeed * Time.deltaTime, Vector3.up));

    }

}
