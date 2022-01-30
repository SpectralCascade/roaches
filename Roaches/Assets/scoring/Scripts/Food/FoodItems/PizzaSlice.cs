using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PizzaSlice : BigFoods
{
    
   
    // Start is called before the first frame update
    void Awake()
    {
        timeToEat = 10f;
        scoreValue = 50;
         // dont think the eattimer is on here anymore may delete /clean up later
    }

    // Update is called once per frame
    void Update()
    {
        // debug testing
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    IsEaten();
        //}
    }

    public override void IsEaten()
    {
        // powerups for specific food go in override methods, or possibly in Player class makes more sense
        // otherwise base virtual method (just increaing score) will execute

        Round.score += scoreValue;
        Debug.Log("Extra logic was hit");
    }

    //IEnumerator EatFood()
    //{
    //    if (beingEaten)
    //    {
    //        if (eatTimer > 0)
    //        {
    //            eatTimer -= Time.deltaTime;
    //            // display UI eating progress bar
    //        }
    //        else
    //        {
    //            eatTimer = 0;
    //            IsEaten();
    //        }
    //    }
    //    else
    //    {
    //        eatTimer = timeToEat;
    //        StopCoroutine(EatFood());
    //    }
    //    yield return null;
    //}
}
