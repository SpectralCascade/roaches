using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    // objectives:
    // "small droplet" foods e.g. cheese/bread crums could spawn randomly on floor level - to avoid on a long list/array of transforms (afternote: but it may be fine to combine with bigfoods anyway - the logic would work)
    // bigger food items could spawn at set locations randomly - these have unique properties i.e. time to eat, possible powerups etc. (probably need polymorphism haven't caught up on this concept for a while oops)

    [SerializeField] List<Transform> foodSpawnPoint = new List<Transform>();

    [SerializeField] GameObject[] foodItemsToSpawn; // think it is fine to seriailize this?
    [SerializeField] int amountOfFoodToSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        InitFoodSpawns();
            

    }

    private void Start()
    {
        PopulateFoodSpawns();
        
    }

    void InitFoodSpawns()
    {
        GameObject[] spawnPointsInLvl = GameObject.FindGameObjectsWithTag("FoodSpawnPoint");

        if(amountOfFoodToSpawn <= spawnPointsInLvl.Length)
        {
            for(int i = 0; i < spawnPointsInLvl.Length; i++)
            {
                foodSpawnPoint.Add(spawnPointsInLvl[i].GetComponent<Transform>());
            }

        }
        else
        {
            Debug.LogError("Not enough spawn points for food amount to spawn!");
        }
    }

    void PopulateFoodSpawns()
    {
        for(int i = 0; i < amountOfFoodToSpawn; i++) 
        {
            int randSpawnPoint = Random.Range(0, foodSpawnPoint.Count);
            int randomFoodItem = Random.Range(0, foodItemsToSpawn.Length);

            Instantiate(foodItemsToSpawn[randomFoodItem], foodSpawnPoint[randSpawnPoint].position, Quaternion.identity);

            foodSpawnPoint.RemoveAt(randSpawnPoint); // remove the spawn point which was used, note: points that are still visible after init/play in the inspector are the spawn points which were not chosen for that spawn (adds more randomness)
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
