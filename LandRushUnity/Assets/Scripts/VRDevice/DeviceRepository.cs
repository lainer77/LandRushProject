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
                    GameObject.Find("[CameraRig]").transform.Find("Controller (left)")
                        .GetComponent<DeviceInteraction>();
            return _leftDeviceInteraction;
        }
    }

    private static RightDeviceInteraction _rightDeviceInteraction;

    public static RightDeviceInteraction RightDeviceInteraction
    {
        get
        {
            if (_rightDeviceInteraction == null)
                _rightDeviceInteraction =
                    GameObject.Find("[CameraRig]").transform.Find("Controller (right)")
                        .GetComponent<RightDeviceInteraction>();

            return _rightDeviceInteraction;
        }
    }
}