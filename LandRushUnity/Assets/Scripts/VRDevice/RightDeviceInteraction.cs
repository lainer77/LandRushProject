using UnityEngine;

public class RightDeviceInteraction : DeviceInteraction
{
    public RaserCraft RaserCraft { get; set; }

    protected override void Awake()
    {
        base.Awake();
        RaserCraft = transform.Find("RaserPoint").GetComponent<RaserCraft>();
    }
}