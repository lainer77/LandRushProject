using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityScriptHelper;

public class ControllerInteraction : MonoBehaviourEx
{
    #region outlets

    public event UnityAction TriggerButtonDown;
    public event UnityAction TriggerButtonUp;
    public event UnityAction TriggerButtonPress;

    public event UnityAction GripButtonDown;
    public event UnityAction GripButtonUp;
    public event UnityAction GripButtonPress;

    public event UnityAction TouchpadButtonDown;
    public event UnityAction TouchpadButtonUp;
    public event UnityAction TouchpadButtonPress;

    public event UnityAction TouchpadLeftButtonPress;
    public event UnityAction TouchpadRightButtonPress;
    public event UnityAction TouchpadUpButtonPress;
    public event UnityAction TouchpadDownButtonPress;

    #endregion

    #region fields

    private SteamVR_Controller.Device _controller;

    public SteamVR_Controller.Device Controller
    {
        get
        {
            if (_controller == null)
                _controller = SteamVR_Controller.Input((int) _trackedObject.index);
            return _controller;
        }
    }

    private SteamVR_TrackedObject _trackedObject;
    public DeviceButton TriggerButton { get; set; }
    public DeviceButton GripButton { get; set; }
    public TouchPadButton TouchpadButton { get; set; }

    #endregion

    #region messages

    protected override void Awake()
    {
        _trackedObject = GetCachedComponent<SteamVR_TrackedObject>();
        ButtonIdSetting();

//        컨트롤러 버튼에 대한 이벤트를 사용할 때 활성화 시킨다.
//        EventSetting();
    }

    void EventCouplering()
    {
//        TriggerButtonDown += TriggerButton.DeviceButtonDown;
    }

    #endregion

    #region methods

    public void ButtonIdSetting()
    {
        TriggerButton = new DeviceButton(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        GripButton = new DeviceButton(Valve.VR.EVRButtonId.k_EButton_Grip);
        TouchpadButton = new TouchPadButton(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
    }

    #endregion
}