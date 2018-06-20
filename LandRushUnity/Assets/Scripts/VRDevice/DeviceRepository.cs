using UnityEngine;

public class DeviceRepository
{
    public static DeviceInteraction LeftDeviceInteraction { get; } =
        GameObject.Find("[CameraRig]").transform.Find("Controller (left)").GetComponent<DeviceInteraction>();
    public static DeviceInteraction RightDeviceInteraction { get; } =
        GameObject.Find("[CameraRig]").transform.Find("Controller (right)").GetComponent<DeviceInteraction>();

}
