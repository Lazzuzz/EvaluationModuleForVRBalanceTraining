using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    //La bruker velge denne, utgjør hvor lenge vi ønsker timeren å telle ned
    public float timeRemaining = 10.0f;                                //Stores the amount of time that remains of the countdown, before the code us ran this desides the lenght of the given countdown
    public bool timerIsRunning = false;                                //

    //Må bli satt til true i oppgavens egen klasse, når oppgave er fullført
    public bool taskIsComplete = false;

    //Constructor
    public Countdown (float newTimeRemaing, bool newTimerIsRunning, bool newTaskIsComplete)
    {
        this.timeRemaining = newTimeRemaing;
        this.timerIsRunning = newTimerIsRunning;
        this.taskIsComplete = newTaskIsComplete;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if (taskIsComplete)
            {
                displayTime(timeRemaining);
                timerIsRunning = false;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    //Used to convert the remaining time in timeRemanining into min and sec, and log to debug window
    public void displayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        Debug.Log("You have completed the task, and have " + minutes + " minutes and " + seconds + " seconds left.");

        //Brukes hvis vi ønsker en visuell timer
        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

