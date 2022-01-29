using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapVR {
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackPositionOffset;
    public Vector3 trackRotationOffset;

    public void Map() {
        rigTarget.position = vrTarget.TransformPoint(trackPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackRotationOffset);
    }
}
