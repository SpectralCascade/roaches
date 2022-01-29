using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerVR : MonoBehaviour
{

    public float headLocalThresholdSpeed = 0.1f;

    [Range(0, 1)]
    public float smoothing = 0.3f;

    [SerializeField]
    private Animator animator;

    private Vector3 previousPos;

    [SerializeField]
    private RigControllerVR rigVR;

    // Start is called before the first frame update
    void Start()
    {
        previousPos = rigVR.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headSpeed = (rigVR.head.vrTarget.position - previousPos) / Time.deltaTime;
        headSpeed.y = 0;
        Vector3 headLocalSpeed = transform.InverseTransformDirection(headSpeed);
        previousPos = rigVR.head.vrTarget.position;

        float previousBlendX = animator.GetFloat("BlendX");
        float previousBlendY = animator.GetFloat("BlendY");

        animator.SetBool("isMoving", headLocalSpeed.magnitude > headLocalThresholdSpeed);
        animator.SetFloat("BlendX", Mathf.Lerp(previousBlendX, Mathf.Clamp(headLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("BlendY", Mathf.Lerp(previousBlendX, Mathf.Clamp(headLocalSpeed.z, -1, 1), smoothing));
    }
}
