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

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;

        head.Map();
        rightArm.Map();
        leftArm.Map();
    }
}
