using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    //[SerializeField] Transform[] cockRoachSpawnPoints;
    [SerializeField] List<Transform> cockRoachSpawnPoints = new List<Transform>(); // turned into list so I can remove spawnPoint when used
    int cockroachAmount;

    // just made human spawn point public for now
    public Transform humanSpawnPoint;

    
    HumanProto hPlayer;
    CochRoachProto[] cPlayers; // could be adjusted to just one player like the hPlayer above, and also may benefit from being a list later down the line (x amount of players, add(), remove() etc.)
    

    private void Awake()
    {
        PopulateSpawnPoints();
        
        hPlayer = FindObjectOfType<HumanProto>();
       
        cPlayers = FindObjectsOfType<CochRoachProto>();
    }

    
    void PopulateSpawnPoints()
    {
        //int spawnPointsInLevel;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("CSpawnPoint"); // should get cleaned up by garbo collection right?
                                                                                     
        
        // array ver: cockRoachSpawnPoints = new Transform[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            cockRoachSpawnPoints.Add(spawnPoints[i].GetComponent<Transform>()); //= spawnPoints[i].GetComponent<Transform>();
        }
        // or this alternative maybe (from minecart game):
        // if this sits on ---spawnpoints--- then GetComponentInChildren<Transform>() and string find in foreach loop


        
    }


    // Start is called before the first frame update
    void Start()
    {
        hPlayer.transform.position = humanSpawnPoint.position; // set human spawnpos

        if (cPlayers.Length <= cockRoachSpawnPoints.Count)
        {
            foreach (CochRoachProto cPlayer in cPlayers)
            {
                int randSpawnPoint = Random.Range(0, cockRoachSpawnPoints.Count);

                cPlayer.transform.position = cockRoachSpawnPoints[randSpawnPoint].position;

                cockRoachSpawnPoints.RemoveAt(randSpawnPoint);
            }
        }
        else
        {
            Debug.LogError("Not enough spawn points for amount of cochroach players!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetPlayerAtSpawnPos()
    {
        // eventually put start logic here
    }
}
