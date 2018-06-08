using UnityEngine;

public class DeviceRepository
{
    public static DeviceInteraction LeftDeviceInteraction { get; } =
        GameObject.FindGameObjectWithTag("LeftController").GetComponent<DeviceInteraction>();
    public static RightDeviceInteraction RightDeviceInteraction { get; } =
        GameObject.FindGameObjectWithTag("RIghtController").GetComponent<RightDeviceInteraction>();

}
