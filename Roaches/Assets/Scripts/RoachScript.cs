using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoachScript : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    float rotation;
    float yRot;
    wiggle legs;
    public Transform rayFrontForward;
    public Transform rayFrontDown;
    public Transform rayBackDown;
    [Range(0, 1)]
    public float rayLengthForward;
    [Range(0, 1)]
    public float rayLengthDown;

    private Vector3 downSurfaceNormal = Vector3.zero;

    private float surfaceTiltDelta = 0;

    Vector3 floorDir;
    Rigidbody rB;
    bool grounded;
    float tiltSpeed = 0;
    bool previouslyHitDown = false;

    // Start is called before the first frame update
    void Start()
    {
        legs = GetComponentInChildren<wiggle>();
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime;
        yRot = Input.GetAxisRaw("Horizontal") * rotSpeed;
        rotation += yRot;
        transform.rotation = Quaternion.Euler(0, rotation, 0);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            legs.moving = true;
        }
        else
        {
            legs.moving = false;
        }

        bool didHitDownFront = Physics.Raycast(rayFrontDown.position, transform.up * -1, out RaycastHit hitFrontDown, rayLengthDown);
        bool didHitDownBack = Physics.Raycast(rayBackDown.position, transform.forward, out RaycastHit hitBackDown, rayLengthDown);
        bool didHitForward = Physics.Raycast(rayFrontForward.position, transform.forward, out RaycastHit hitForward, rayLengthForward);

        float angularSpeed = 1;
        float delta = Time.deltaTime * angularSpeed;

        if (didHitForward) {
            Debug.Log("Hit forward. Trying to rotate from " + transform.up + " to " + hitForward.normal);
            transform.rotation = transform.rotation * Quaternion.LookRotation(Vector3.RotateTowards(transform.up, hitForward.normal, delta, 1).normalized);// Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.up), delta);
        }
        if (didHitDownBack) {
            downSurfaceNormal = hitBackDown.normal;
        } else {
            downSurfaceNormal = Vector3.zero;
        }

        //float g = 9.81f * 0.05f;
        //bool didHitDown = Physics.Raycast(rayCheck.position, transform.up * -1, out RaycastHit hitDown, 1000f);
        //bool didHitForward = Physics.Raycast(rayCheck.position, transform.forward, out RaycastHit hitForward, 0.5f);
        //bool hitRear = Physics.Raycast(rayCheck.position+transform.forward*-26, ((transform.forward / -2)+transform.up * -1), out RaycastHit rHr, 1f);
        //bool hitMid = Physics.Raycast(rayCheck.position, ( transform.up * -1), out RaycastHit rHm, 1f);
        /*if (didHitForward)
        {
            //rB.useGravity = false;

            Vector3 toSurface = (hitForward.point - rayCheck.position).normalized;
            transform.rotation = Quaternion.FromToRotation(transform.up, hitForward.normal) * transform.rotation.normalized;
            rB.AddForce(toSurface * g, ForceMode.Acceleration);
        } else if (didHitDown) {
            Vector3 toSurface = (hitDown.point - rayCheck.position).normalized;
            transform.rotation = Quaternion.FromToRotation(transform.up, hitDown.normal) * transform.rotation.normalized;
            rB.AddForce(toSurface * g, ForceMode.Acceleration);
            //rB.useGravity = true;
            //downPos = Vector3.zero;
        } else {
            if (previouslyHitDown) {
                Debug.Log("Quick flip around, current pos = " + rB.position.ToString());
                transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation.normalized;
                rB.position = (transform.position + (rayCheck.position - transform.position));
                Debug.Log("Quick flip around, new pos = " + rB.position.ToString());

            }
            rB.AddForce((transform.parent.position - rayCheck.position).normalized * g, ForceMode.Acceleration);
        }
        previouslyHitDown = didHitDown;*/

        /*if (hit)
        {
            //grounded = true;
            rB.useGravity = false;
            rB.velocity = Vector3.zero;
            rB.angularVelocity = Vector3.zero;
        }
        else
        {
            //grounded = false;
            rB.useGravity = true;
        }*/




    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 10)
        {
            Destroy(gameObject);
        }
    }


}
