using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackDeviceDrop : MonoBehaviour
{

    public Dropdown dropdown;

    public trackedDevice tracedDevice;


    // Start is called before the first frame update
    void Start()
    {
        DeviceTypList();

        dropdown = GetComponent<Dropdown>();
    }

    void DeviceTypList()
    {
        string[] enumDeviceTypes = Enum.GetNames(typeof(trackedDevice));
        List<String> devicetyps = new List<string>(enumDeviceTypes);
        dropdown.AddOptions(devicetyps);
    }

   
}
