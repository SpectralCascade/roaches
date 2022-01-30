using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BigFoods : MonoBehaviour
{
    // Food model assets from: https://www.kenney.nl/assets/food-kit


    // will think about eatTimer, will first do simple collision, then look at hold a button to eat (will require a little thought for now 2am lol)
    //protected float eatTimer; // now the roach accesses the timeToEat when in collider
    protected bool beingEaten = false;
    [SerializeField] public float timeToEat; // needed to make public for canvas text oops
    [SerializeField] protected int scoreValue;

    //void Update()
    //{
    //    // debug testing
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        IsEaten();
    //    }
    //}

    public virtual void IsEaten()
    {
        Round.score += scoreValue;
    }

    // deprecatiated I think
    //public virtual void SetEating(bool isEating)
    //{
    //    beingEaten = isEating;
    //    // may need destroy here (or if using a "node like" structure where food keeps alive, just apply a cooldown function)
    //}

    // this may be gross I am unsure, but need to get it for CochRoachProto class

    public float GetTimeToEat()
    {
        return timeToEat;
    }

}
    
