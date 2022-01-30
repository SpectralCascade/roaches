using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CochRoachProto : MonoBehaviour
{
    // this is a cochroach player class
    float cockroachEatingTimer;

    [SerializeField] Text eatTimer; // oh crap I want this on the food item, but don't know how I would make it only display on one persons screen (maybe a different canvas could belong to one player/camera and get then pass this.gameObject/CochroachProto in function?)
                                    // also need multiple canvas (hope that isnt too performance heavy) - for now only cPlayer 1 (P2) has reference to this text

    BigFoods foodToInteract;

    private SpawnPoints spawnPointsScript;

    // Start is called before the first frame update
    void Awake()
    {
        spawnPointsScript = FindObjectOfType<SpawnPoints>();
    }

    // can't remember if collision with a pizza object should be OnCollision or OnTrigger - I think trigger if going down timeToEat path
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FoodItem"))
        {
            foodToInteract = other.GetComponent<BigFoods>();

            //Destroy(other.gameObject);

            cockroachEatingTimer = foodToInteract.timeToEat;
            eatTimer.gameObject.SetActive(true);
            eatTimer.text = "HOLD E TO EAT!\nFOOD REMAINING: ";
        }
            // pizza powerup logic could go here (as an example - any food object but may require extra if statments or if possible could take adv of the polymorphism but the food classes' may need to communicate back - unsure yet)
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("FoodItem"))
        {

            if (cockroachEatingTimer > 0)
            {
                cockroachEatingTimer -= Time.deltaTime;
                float timeToDisplay = cockroachEatingTimer + 1; // options are (though some if logic would solve this but unsure as it is in an every frame call) are have it count to zero, or have it count to 1 (0 technically but invisiblity) however this fake "adds" a second to the UI element
                eatTimer.text = string.Format("FOOD REMAINING: {0:0}", timeToDisplay); // apologies if this is gross
            }
            else
            {
                ResetTimer();
                foodToInteract.IsEaten(); // eaten food

                // apply eating logic
                // cockroachEatingTimer = food.GetTimeToEat();
            }

            //Destroy(other.gameObject);
        }
        // pizza powerup logic could go here (as an example - any food object but may require extra if statments or if possible could take adv of the polymorphism but the food classes' may need to communicate back - unsure yet)
    }

    // need way to die
    public void Death()
    {
        Round.humanScore++;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        Debug.Log("before Waitforseconds");
        yield return new WaitForSeconds(2f);
        Debug.Log("after waitforseconds");
        spawnPointsScript.SetPlayerAtSpawnPos(transform);
        Debug.Log("after SetPlayerAtSpawn");
    }

    // NOT WORKING AS INTENDED, still useful for one of the calls maybe, but instead of a cooldown, the timeToEat could be artifically increased either by default (longer time to eat), or 
    void ResetTimer()
    {
        cockroachEatingTimer = foodToInteract.GetTimeToEat();
    }

    private void OnTriggerExit(Collider other)
    {
        foodToInteract = null;
        eatTimer.gameObject.SetActive(false);
    }
}
