using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    //possibly add a game object clock in the room which displays the timer - interactable/ can be thrown by seeker (achievement if squashed by timer ;) )
    // need to add win condition things
    // possibly turn this into a singleton (want a static score int variable)

    [SerializeField] float timer = 30;
    bool runTimer = false;

    [SerializeField] Text timerText;
    float minutes, seconds;

    [SerializeField] private int scoreWinGoal = 1000;
    [SerializeField] public static int score;
    [SerializeField] Text scoreText;

    [SerializeField] public static int humanScore = 0; // i.e. roaches killed
    [SerializeField] private int roachLivesCollective = 6; // how many times the roaches can respawn, collectively for now time constraints!
    [SerializeField] Text livesCount;

    [SerializeField] Text roundOverMessage;

    private SpawnPoints spawnPointsScript;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        runTimer = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(timer > 0 && runTimer == true)
        {
            timer -= Time.deltaTime;
            UpdateTimeAndScore(timer);


            if(score >= scoreWinGoal)
            {
                
                CockroachWin(0); // roaches win by reaching a certain goal target

            }

            if(humanScore >= roachLivesCollective)
            {
                HumanWin();
            }
        }
        else
        {
            timer = 0;
            CockroachWin(1); // roaches win by running down the clock
        }
    }

    
    
    void UpdateTimeAndScore(float timerValue)
    {
        timerValue += 1; // add a second to the timer (the one that will be displayed, as it is counting down - accounting for whole seconds)

            //minutes = Mathf.FloorToInt(timer / 60);
            //seconds = Mathf.FloorToInt(timer % 60); may have a clock object, would be cool so would access these variables publically
        float minutes = Mathf.FloorToInt(timerValue / 60);
        float seconds = Mathf.FloorToInt(timerValue % 60);

        timerText.text = string.Format("TIME REMAINING: {0:00}:{1:00}", minutes, seconds);
        scoreText.text = "COCKROACH SCORE: " + score + " / " + scoreWinGoal;

        int livesCountToDisplay = roachLivesCollective - humanScore;
        if (livesCountToDisplay < 0)
            livesCountToDisplay = 0;

        livesCount.text = "ROACH LIVES REMAINING: " + livesCountToDisplay;
    }

    void CockroachWin(int wonBy)
    {
        runTimer = false; // stop timer (for score victory timer will still run)
        roundOverMessage.gameObject.SetActive(true);
        if(wonBy == 0)
        {
            // Debug.Log("Round Over, roaches have invaded the house/reached the goal!");
            roundOverMessage.text = "ROUND OVER, ROACHES WON!";
        }
        if (wonBy == 1)
        {
            roundOverMessage.text = "ROUND OVER, ROACHES WON!";
        }
    }

    void HumanWin()
    {
        roundOverMessage.gameObject.SetActive(true);
        roundOverMessage.text = "ROUND OVER, HUMAN WON!";
    }
}
