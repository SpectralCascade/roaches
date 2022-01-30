using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiggle : MonoBehaviour
{
    public bool moving = false;

    public float speed;
    bool leftRight = true;
    float yRot = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //wiggles the cockroaches legs when moving == true. 
    {
        if(moving)
        {
            transform.Rotate(0, yRot, 0);

        }

        if(leftRight)   //leftright is just a bool which is toggled so that the legs move back and forth.
        {
            yRot += speed; // changing speed makes the wiggles faster
        }
        else
        {
            yRot -= speed;
        }

        if(yRot > 0.05 || yRot < -0.05) //changing numbers here makes the legs wiggle wider or narrower
        {
            leftRight = !leftRight;
        }
        
    }
}
