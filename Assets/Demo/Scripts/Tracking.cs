
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;


//Class that contains some functions used in other classes 

public class Tracking : MonoBehaviour
{

    private InputDevice inputDevice;
    private List<Vector3> pos = new List<Vector3>();

    //Used in NaiveDistance
    private bool isPressed = true;
    private bool wasPressed = false;
    private int counter = 0;

    //USED IN TotalDistance
    private float totalMovedDistance;
    private bool track;
    private Vector3 lastPos;
    //public trackedDevice tracedDevice = trackedDevice.RIGHTHAND;



    // Start is called before the first frame update
    void Start()
    {

    }

    public void setup(trackedDevice tracedDevice)
    {
        checkTrackedDevice(tracedDevice);
   
    }

    trackedDevice checkTrackedDevice(trackedDevice tracedDevice)
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
    }

 

    public List<Vector3> returnTracked(trackedDevice device)
    {
        switch (device)
        {
            case trackedDevice.HEAD:
                pos.Add(InputTracking.GetLocalPosition(XRNode.Head));
                return pos;


            case trackedDevice.LEFTHAND:
                pos.Add(InputTracking.GetLocalPosition(XRNode.LeftHand));
                return pos;


            default:
                pos.Add(InputTracking.GetLocalPosition(XRNode.RightHand));
                return pos;


        }

    }

    //Used to return start and stop position of a users movement
    public Vector3 returnPos(trackedDevice device)
    {
        switch (device)
        {
            case trackedDevice.HEAD:
                return InputTracking.GetLocalPosition(XRNode.Head);


            case trackedDevice.LEFTHAND:
                return InputTracking.GetLocalPosition(XRNode.LeftHand);


            default:
                return InputTracking.GetLocalPosition(XRNode.RightHand);


        }

    }

    //Calculates the longest distance traveled by using the pos List
    public float distance(List<Vector3> pos)
    {
        Vector3 first = pos.First();
        float largest = 0;

        foreach (Vector3 vector in pos)
        {
            if (Vector3.Distance(vector, first) > largest)
            {
                largest = Vector3.Distance(vector, first);
            }
        }
        return largest;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
