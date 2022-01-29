using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigFeetIK : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public float rightFootPosWeight = 1;
    public float leftFootPosWeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnAnimatorIK(int layerIndex) {

        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        if (Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out RaycastHit hit)) {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point);
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        if (Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit)) {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point);
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}
