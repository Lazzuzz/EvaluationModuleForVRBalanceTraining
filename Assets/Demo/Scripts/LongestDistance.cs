
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;
using UnityEngine.UI;

//Tracking longest distance moved from the first push of a button 
public class LongestDistance : MonoBehaviour
{
    private InputDevice inputDevice;
    private List<Vector3> pos = new List<Vector3>();
    public trackedDevice tracedDevice;

    private bool isPressed = true;
    private bool wasPressed = false;

    Tracking tracking = new Tracking();

    public double longestDistance = 0; //used in visualization

    public Text distaceText;
    string _distaceText = "";

    //REMOVE
    //main startTrace;

    // Start is called before the first frame update

    void Start()
    {

        tracking.setup(tracedDevice);
        //tracking.setup(startTrace.traced);

        List<InputDevice> devices = new List<InputDevice>();

        if (tracedDevice == trackedDevice.LEFTHAND)
        {
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);

        }
        else
        {
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        }

        Debug.Log(devices.Count());


        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }


    // Update is called once per frame
    void Update()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isPressed);

        if (inputDevice != null && isPressed && !wasPressed)
        {
            wasPressed = true;
        }

        if (inputDevice != null && wasPressed)
        {
            pos = tracking.returnTracked(tracedDevice);
            longestDistance = tracking.distance(pos);
            _distaceText = "The distance moved :" + tracking.distance(pos);
            distaceText.text = _distaceText;
            Debug.Log(tracking.distance(pos));
        }

    }
}
