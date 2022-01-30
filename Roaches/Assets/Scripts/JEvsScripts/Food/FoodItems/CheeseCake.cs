using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseCake : BigFoods {

    private void Awake()
    {
        timeToEat = 13f;
        scoreValue = 75;
    }


    // this update method overrides the BigFoods class (Unity uses a technique called Reflection - cool!
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G)) // debug key to test for cheescake
    //    {
    //        IsEaten();
    //    }
    //}
}
