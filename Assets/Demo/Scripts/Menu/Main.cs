using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public Canvas GameMenuCanvas;
    public Button startButton;
    public GameObject obj;
    public GameObject collierCube;
    public Dropdown gameDropdown;
    public Dropdown deviceDropdown;
    public trackedDevice tracedDevice;

    ColliderTracker ct = new ColliderTracker();
    LongestDistance ld = new LongestDistance();
    NaiveDistance nd = new NaiveDistance();
    TotalDistance td = new TotalDistance();


    //GameDropdown gameDrop = new GameDropdown();
    int GValue;
    int DValue;
    //SaveValue gameDropValue = new SaveValue();


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GValue = gameDropdown.value;
        DValue = deviceDropdown.value;
    }

    public void startGame()
    {
        //Debug.Log(GValue);

        //int gameTypeValue = gameDropValue.getDropdown().value;
        handleInputData(GValue);

        GameMenuCanvas.enabled = false;
        
    }

    public void handleInputData(int val)
    {
        if(val == 0)
        {
            Debug.Log("An error in choosing the game option");
        }

        if (val == 1)
        {
            ld = obj.GetComponent<LongestDistance>();
            ld.enabled = true;
            Debug.Log("Chose game option 1");
        }
        if (val == 2)
        {
            td = obj.GetComponent<TotalDistance>();
            td.enabled = true;
            Debug.Log("Chose game option 2");
        }
        if (val == 3)
        {
            ct = obj.GetComponent<ColliderTracker>();
            ct.enabled = true;
            collierCube.SetActive(true);
            Debug.Log("Chose game option 3");
        }
        if (val == 4)
        {
            nd = obj.GetComponent<NaiveDistance>();
            nd.enabled = true;
            Debug.Log("Chose game option 4");
        }
    }

    public void checkTrackedDevice(int val)
    {
        if (val == 0)
        {
            Debug.Log("An error in choosing the device option");
        }
        if (val == 1)
        {
            tracedDevice = trackedDevice.HEAD;
        }

        if (val == 2)
        {
            tracedDevice = trackedDevice.RIGHTHAND;
        }
        if (val == 3)
        {
            tracedDevice = trackedDevice.LEFTHAND;
        }
    }
}
