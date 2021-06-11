
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using System;

public class TotalDistance : MonoBehaviour
{
    // Stores the overall moved distance
    private float totalMovedDistance;

    // flag to start and stop Tracking

    private bool track;
    private bool isPressed;
    private bool wasPressed;
    private InputDevice inputDevice;
    public trackedDevice tracedDevice;
    private int counter = 0;

    // Store position of last frame
    private Vector3 lastPos;
    Tracking t = new Tracking();

    //REMOVE AFTER DEMO
    //main startTrace;

    public double totalDistance = 0; //used in visualization

    private void Start()
    {

        /* List <InputDevice> devices = new List<InputDevice>();
         InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
         Debug.Log(devices.Count());


         foreach (var item in devices)
         {
             Debug.Log(item.name + item.characteristics);
         }

         if (devices.Count > 0)
         {
             inputDevice = devices[0];
         }*/

        //SWITCH TRACED DEVICE AFTER DEMO
        //t.setup(startTrace.traced);
        t.setup(tracedDevice);

        List<InputDevice> devices = new List<InputDevice>();

        if (tracedDevice == trackedDevice.LEFTHAND)
        {
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);

        }
        else
        {
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        }


        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }



    }



    private void Update()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isPressed);

        if (inputDevice != null && isPressed && !wasPressed)
        {

            wasPressed = true;
        }


        if (inputDevice != null && !isPressed && wasPressed)
        {
            if (counter % 2 == 0)
            {
                wasPressed = false;
                BeginTrack();
                counter++;
            }
            else
            {
                wasPressed = false;
                EndTrack();
                counter++;
            }
        }

        // If not Tracking do nothing
        if (!track) return;

        // get current controller position
        var currentPos = InputTracking.GetLocalPosition(XRNode.RightHand);

        // Get distance moved since last frame
        var thisFrameDistance = Vector3.Distance(currentPos, lastPos);

        // sum it up to the total value
        totalMovedDistance += thisFrameDistance;

        // update the last position
        lastPos = currentPos;
    }


    public void BeginTrack()
    {
        // reset total value
        totalMovedDistance = 0;

        // store first position
        lastPos = InputTracking.GetLocalPosition(XRNode.RightHand);

        // start Tracking
        track = true;
    }

    public void EndTrack()
    {
        // stop Tracking
        track = false;

        //totalMovedDistance = Math.Round(totalMovedDistance, 2);

        // whatever you want to do with the total distance now
        totalDistance = Math.Round(totalMovedDistance, 2);


        Debug.Log($"Total moved distance: " + Math.Round(totalMovedDistance, 2), this);
    }

}
