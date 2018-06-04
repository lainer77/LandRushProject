using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class DeviceButton
{
    protected bool Up
    {
        set
        {
            if (value)
                OnDeviceButtonUp();
            _up = value;
        }
    }

    protected bool Down
    {
        set
        {
            if (value)
                OnDeviceButtonDown();
            _down = value;
        }
    }

    protected bool Press
    {
        set
        {
            if (value)
                OnDeviceButtonPress();
            _press = value;
        }
    }


    protected readonly Valve.VR.EVRButtonId _deviceButtonId;
    private bool _up;
    private bool _down;
    private bool _press;

    public DeviceButton(Valve.VR.EVRButtonId deviceButtonId)
    {
        _deviceButtonId = deviceButtonId;
    }

    /// <summary>
    /// 컨트롤러 중 특정버튼의 상태를 읽어드리는 일괄작업
    /// </summary>
    /// <param name="controller">해당 컨트롤러(헤드셋, R컨트롤러, L컨트롤러)</param>
    public virtual void OnPress(SteamVR_Controller.Device controller)
    {
        Down = controller.GetPressDown(_deviceButtonId);
        Up = controller.GetPressUp(_deviceButtonId);
        Press = controller.GetPress(_deviceButtonId);
    }
    public delegate void ActionTest();
    public event ActionTest DeviceButtonDown;

    protected virtual void OnDeviceButtonDown()
    {
        DeviceButtonDown?.Invoke();
    }
    public virtual void SetDeviceButtonDownEvent(ActionTest action, bool addOrRemove)
    {
        if (addOrRemove)
        {
            DeviceButtonDown += action;
        }
        else
        {
            DeviceButtonDown -= action;
        }
    }

    protected event UnityAction DeviceButtonUp;

    protected virtual void OnDeviceButtonUp()
    {
        DeviceButtonUp?.Invoke();
    }

    public virtual void SetDeviceButtonUpEvent(UnityAction action, bool addOrRemove)
    {
        if (addOrRemove)
            DeviceButtonUp += action;
        else
            DeviceButtonUp -= action;
    }
    protected event UnityAction DeviceButtonPress;

    protected virtual void OnDeviceButtonPress()
    {
        DeviceButtonPress?.Invoke();
    }
    public virtual void SetDeviceButtonPressEvent(UnityAction action, bool addOrRemove)
    {
        if (addOrRemove)
            DeviceButtonPress += action;
        else
            DeviceButtonPress -= action;
    }
}