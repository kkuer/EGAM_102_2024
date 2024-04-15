using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public GameObject loseScreen;
    public GameObject loseText;
    public GameObject restartButton;

    public TMP_Text timeText;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        Time.timeScale = 1;
    }
    void Update()
    {
        //check to see if timer is running and end the game when the timer ends
        if (timerIsRunning)
        {
            DisplayTime(timeRemaining);
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        //end game and display lose screen
        else
        {
            Time.timeScale = 0;
            loseScreen.SetActive(true);
            loseText.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    //display the timer as each second ticks down
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
