using UnityEngine;

public class DeviceRepository
{
    private static DeviceInteraction _leftDeviceInteraction;

    public static DeviceInteraction LeftDeviceInteraction
    {
        get
        {
            if (_leftDeviceInteraction == null)
                _leftDeviceInteraction =
                    GameObject.FindGameObjectWithTag(Tags.LeftController).GetComponent<DeviceInteraction>();
            return _leftDeviceInteraction;
        }
    }

    private static DeviceInteraction _rightDeviceInteraction;

    public static DeviceInteraction RightDeviceInteraction
    {
        get
        {
            if (_rightDeviceInteraction == null)
                _rightDeviceInteraction =
                    GameObject.FindGameObjectWithTag(Tags.RightController).GetComponent<DeviceInteraction>();
            return _rightDeviceInteraction;
        }
    }
}