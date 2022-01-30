using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision) {
        if (gameObject.tag == "ThrownItem") {
            RoachController roach = collision.gameObject.GetComponent<RoachController>();
            if (roach != null) {
                OpenGET.Log.Info("ROACH HIT BY PLAYER THROWN OBJECT");
                roach.OnRoachHit(GetComponent<Rigidbody>().velocity);
            }
        }
        if (!string.IsNullOrEmpty(gameObject.tag)) {
            gameObject.tag = "Player";
        }
    }

}
