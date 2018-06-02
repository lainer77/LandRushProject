using UnityEngine;

public class DeviceRepository
{
    public static DeviceInteraction LeftDeviceInteraction { get; } =
        GameObject.Find("[CameraRig]").transform.Find("Controller (left)").GetComponent<DeviceInteraction>();
    public static RightDeviceInteraction RightDeviceInteraction { get; } =
        GameObject.Find("[CameraRig]").transform.Find("Controller (right)").GetComponent<RightDeviceInteraction>();

}
