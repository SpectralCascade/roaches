using OpenGET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowItemController : MonoBehaviour
{

    XRGrabInteractable[] grabItems = null;

    public void Start() {
        grabItems = GameObject.FindObjectsOfType<XRGrabInteractable>();
        for (int i = 0, counti = grabItems.Length; i < counti; i++) {
            grabItems[i].gameObject.AddComponentOnce<ThrowableItem>();
            grabItems[i].selectExited.AddListener(new UnityEngine.Events.UnityAction<SelectExitEventArgs>(OnItemDropped));
        }
    }

    public void OnItemDropped(SelectExitEventArgs exitArgs) {
        exitArgs.interactableObject.transform.gameObject.tag = "ThrownItem";
        Log.Info("ITEM DROPPED BY PLAYER");
    }

}
