using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigControllerVR : MonoBehaviour
{

    public MapVR head;
    public MapVR leftArm;
    public MapVR rightArm;

    public Transform headConstraint;

    public Vector3 headBodyOffset;

    void Start()
    {
        headBodyOffset = headBodyOffset + (transform.position - headConstraint.position);
    }

    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;

        head.Map();
        rightArm.Map();
        leftArm.Map();
    }
}
