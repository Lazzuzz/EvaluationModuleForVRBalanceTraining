using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

//FLYTT TIL EGEN DOKUMENTASJON NÅR VI LAGER DET
/*Description:
Used to track a users actions in correlation to a given 3D object. Lets the user define different criterias defining Tracking during a given task.
Consists of three primary stop types:
    - Wall: Tracks a users positions from when they enter the object, until they exit the object.
    - Time: Tracks a users time and positons from when they enter the object, until they either run out of time or exit the object. Can be set to use either countdown mode or stopwatch mode.
        - Countdown: Lets the user set a predefined time intervall. When the user enters the given object the countdown starts, and will either stop on the completion of the task, upon exiting the object or all of the given time has been spent.
        - Stopwatch: Start a stopwatch timer when the user enters the given object. Will stop when the user either completes a task inside the object, or exits the object.
    - Score: Tracks a users current Score, and compares it against a target Score.

The class has been set up in suach a way that the user can choose between the different stop types in a dropdown menu inside the application. The different variabels like countdownTime has also been setup as public serializables,
which lets the user set different values from a menu.
*/


public class ColliderTracker : MonoBehaviour
{
    //Values used to store important values relating to class functionality

    private InputDevice inputDevice;                            //Used to store what input device is currently used to pass inputs to Unity
    private List<Vector3> pos = new List<Vector3>();            //Used to store the vector position data for the currently tracedDevice
    public trackedDevice tracedDevice;                          //Used to store what device is currently having its position being tracked

    public int inputScore = 0;
    Score scoreTracker = new Score(0, 0);// = new Score (0, 0);

    double distanceMovedInArea = 0; // used for visualization


    //Values used to let the user choose variabel values in the UI

    public stopType stopTyp;                                    //Used to store which stopType is being used, either WALL, TIME or SCORE 
    public timeType timeTyp;                                    //Used to store which timeType is being used, either COUNTDOWN or STOPWATCH
    //public GameObject wallPrefab;                               //Used to store which 3D object is being used as a trigger to track pos, time  and Score


    //Values used in creating a countdown object

    public float timeRemaining = 0;                          //Used as an instantiatior for the desired time used in countdown, given in float 
    private bool timerIsRunning = false;                //Used as an instantiatior in countdown, tracks if the timer is running
    private bool taskIsComplete = false;                //Used as an instantiatior in countdown, tracks if the given task has been completed

    //TODO: Finn en måte å bruke countDownTime og de andre variablene for å lage et countdown objekt
    //Constructor
    //public Countdown countdown = new Countdown(1, false, false);
    public Stopwatch stopwatch = new Stopwatch();
    Tracking t = new Tracking();


    public float colliderDistance = 0; // used in visualization

    //Enum that allows the user to choose between different stopTypes for a given task
    [Serializable]
    public enum stopType
    {
        WALL,
        TIME,
        SCORE
    }


    //Enum that allows the user to choose between different timeTypes used to track the time progress for a given task
    [Serializable]
    public enum timeType
    {
        STOPWATCH,
        COUNTDOWN
    }


    // Start is called before the first frame update
    void Start()
    {

        checkStopType(stopTyp);
        if (stopTyp == stopType.TIME)
        {
            checkTimeType(timeTyp);
        }
        if (stopTyp == stopType.SCORE)
        {
            scoreTracker.setTargetScore(inputScore);
        }
        t.setup(tracedDevice);
    }


    //OnTriggeEnter is called once when the trigger condition is met
    private void OnTriggerEnter(Collider obj)
    {

        Debug.Log("Passed wall");

        if (stopTyp == stopType.TIME)
        {
            if (timeTyp == timeType.COUNTDOWN)
            {
                timerIsRunning = true;
                Debug.Log("Countdown has started at " + timeRemaining);

            }
            else
            {
                stopwatch.Start();
                Debug.Log("Stopwatch has started!");
            }
        }
        else if (stopTyp == stopType.SCORE)
        {
            scoreTracker.scoreIterator();
        }
        else
        {
            stopwatch.Start();
        }
    }


    //OnTriggerStay is called once per frame as long as the trigger condition is met
    private void OnTriggerStay(Collider other)
    {
        pos = t.returnTracked(tracedDevice);

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining = timeRemaining - Time.deltaTime;
            }
            /*else if (taskIsComplete)
            {
                displayTime(timeRemaining);
                timerIsRunning = false;
            }*/
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        /*
        if (countdown.timerIsRunning == false)
        {
            countdown.displayTime(countdown.timeRemaining);
        }
        */
    }


    //OntriggerExit is called once when the trigger condition is no longer met
    private void OnTriggerExit(Collider other)
    {
        if (stopTyp == stopType.TIME)
        {
            if (timeTyp == timeType.COUNTDOWN)
            {
                taskIsComplete = true;
                timerIsRunning = false;
                displayTime(timeRemaining);



            }

            if (timeTyp == timeType.STOPWATCH)
            {
                stopwatch.Stop();
            }
        }

        else if (stopTyp == stopType.SCORE)
        {
            //scoreTracker.scoreIterator(scoreTracker.currentScore);

        }

        else
        {
            Debug.Log("Exiting wall");
            stopwatch.Stop();
            Debug.Log("Time spent inside the object :" + stopwatch.Elapsed.ToString("mm\\:ss\\.ff"));
            distanceMovedInArea = t.distance(pos);
            colliderDistance = t.distance(pos);
            Debug.Log("The longest distance moved past entering the wall :" + t.distance(pos));
            pos.Clear();
        }
    }


    //Used to check what stopType has been set in the UI
    stopType checkStopType(stopType stopTyp)
    {
        switch (stopTyp)
        {
            case stopType.TIME:
                return stopTyp = stopType.TIME;

            case stopType.SCORE:
                return stopTyp = stopType.SCORE;

            default:
                return stopTyp = stopType.WALL;
        }
    }

    //Used to check what timeType has been set in the UI
    timeType checkTimeType(timeType timeTyp)
    {
        switch (timeTyp)
        {
            case timeType.COUNTDOWN:
                /*
                timeRemaining = ;
                countdown.timerIsRunning = countdownTimerIsRunning;
                countdown.taskIsComplete = countdownTaskIsComplete;
                */
                return timeTyp = timeType.COUNTDOWN;

            default:
                return timeTyp = timeType.STOPWATCH;
        }
    }

    public void displayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);


        Debug.Log("You have completed the task, and have " + minutes + " minutes and " + seconds + " seconds left.");

        //Brukes hvis vi ønsker en visuell timer
        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /* trackedDevice checkTrackedDevice(trackedDevice tracedDevice)
     {
         switch (tracedDevice)
         {
             case trackedDevice.HEAD:
                 return tracedDevice = trackedDevice.HEAD;


             case trackedDevice.LEFTHAND:
                 return tracedDevice = trackedDevice.LEFTHAND;


             default:
                 return tracedDevice = trackedDevice.RIGHTHAND;

         }
     }*/





}