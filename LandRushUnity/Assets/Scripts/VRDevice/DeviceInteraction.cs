using UnityEngine;
using UnityEngine.Events;
using UnityScriptHelper;


public class DeviceInteraction : MonoBehaviourEx
{
    #region outlets
    
    #endregion

    #region fields
    private SteamVR_Controller.Device _controller;

    public SteamVR_Controller.Device Controller
    {
        get
        {
            if (_controller == null)
                _controller = SteamVR_Controller.Input((int)TrackedObject.index);
            return _controller;
        }
    }

    private SteamVR_TrackedObject _trackedObject;

    public SteamVR_TrackedObject TrackedObject
    {
        get
        {
            if (_trackedObject == null)
                _trackedObject = GetCachedComponent<SteamVR_TrackedObject>();
            return _trackedObject;
        }
    }
    private DeviceButton _triggerButton;
    private DeviceButton _gripButton;
    private TouchPadButton _touchpadButton;
    private DeviceButton _applicationMenuButton;
    private DeviceButton _systemMenuButton;
    private TouchPadButton _touchPadButtonUp;
    private TouchPadButton _touchPadButtonDown;
    private TouchPadButton _touchPadButtonLeft;
    private TouchPadButton _touchPadButtonRight;

    public DeviceButton TriggerButton
    {
        get
        {
            if (_triggerButton == null)
                _triggerButton = new DeviceButton(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            return _triggerButton; }
    }

    public DeviceButton GripButton
    {
        get
        {
            if (_gripButton == null)
                _gripButton = new DeviceButton(Valve.VR.EVRButtonId.k_EButton_Grip);
            return _gripButton; }
    }

    public TouchPadButton TouchpadButton
    {
        get
        {
            if (_touchpadButton == null)
                _touchpadButton = new TouchPadButton(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
            return _touchpadButton; }
    }

    public TouchPadButton TouchpadButtonUp
    {
        get
        {
            if (_touchPadButtonUp == null)
                _touchPadButtonUp = new TouchPadButton(Valve.VR.EVRButtonId.k_EButton_DPad_Up);
            return _touchPadButtonUp;
        }
    }

    public TouchPadButton TouchpadButtonDown
    {
        get
        {
            if (_touchPadButtonDown == null)
                _touchPadButtonDown = new TouchPadButton(Valve.VR.EVRButtonId.k_EButton_DPad_Down);
            return _touchPadButtonDown;
        }
    }
    public TouchPadButton TouchpadButtonLeft
    {
        get
        {
            if (_touchPadButtonLeft == null)
                _touchPadButtonLeft = new TouchPadButton(Valve.VR.EVRButtonId.k_EButton_DPad_Left);
            return _touchPadButtonLeft;
        }
    }

    public TouchPadButton TouchpadButtonRight
    {
        get
        {
            if (_touchPadButtonRight == null)
                _touchPadButtonRight = new TouchPadButton(Valve.VR.EVRButtonId.k_EButton_DPad_Right);
            return _touchPadButtonRight;
        }
    }

    public DeviceButton ApplicationMenuButton
    {
        get
        {
            if (_applicationMenuButton == null)
                _applicationMenuButton = new DeviceButton(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
            return _applicationMenuButton; }
    }

    public DeviceButton SystemMenuButton
    {
        get
        {
            if (_systemMenuButton == null)
                _systemMenuButton = new DeviceButton(Valve.VR.EVRButtonId.k_EButton_System);
            return _systemMenuButton; }
    }

    #endregion

    #region messages

    protected override void Update()
    {
        if (Controller == null)
        {
            Debug.Log("Controller not lnitialized");
            return;
        }

        TriggerButton.OnPress(Controller);
        GripButton.OnPress(Controller);
        TouchpadButton.OnPress(Controller);
        ApplicationMenuButton.OnPress(Controller);
        SystemMenuButton.OnPress(Controller);
        TouchpadButtonUp.OnPress(Controller);
        TouchpadButtonDown.OnPress(Controller);
        TouchpadButtonLeft.OnPress(Controller);
        TouchpadButtonRight.OnPress(Controller);
    }

    #endregion

    #region methods
    

    
    #region TouchpadButton 봉인
//
//    /// <summary>
//    /// TouchpadButton 버튼이 터치되었을 떄 호출되는 함수
//    /// </summary>
//    public void OnTouchpadButtonIn(UnityAction action, bool addOrRemove)
//    {
//        if (addOrRemove)
//            TouchpadButton.DPadButtonIn += action;
//        else
//            TouchpadButton.DPadButtonIn -= action;
//    }
//
//    /// <summary>
//    /// TouchpadButton 버튼에서 벗어났을 떄 호출되는 함수
//    /// </summary>
//    public void OnTouchpadButtonOut(UnityAction action, bool addOrRemove)
//    {
//        if (addOrRemove)
//            TouchpadButton.DPadButtonOut += action;
//        else
//            TouchpadButton.DPadButtonOut -= action;
//    }
//
//    /// <summary>
//    /// TouchpadButton 버튼이 터치된 상태일 떄 호출되는 함수
//    /// </summary>
//    public void OnTouchpadButtonDrag(UnityAction action, bool addOrRemove)
//    {
//        if (addOrRemove)
//            TouchpadButton.DPadButtonDrag += action;
//        else
//            TouchpadButton.DPadButtonDrag -= action;
//    }
//
//    /// <summary>
//    /// TouchpadButton 버튼이 터치된 상태에서 위로 움직였을 떄 호출되는 함수
//    /// </summary>
//    public void OnTouchpadButtonDragUp(UnityAction action, bool addOrRemove)
//    {
//        if (addOrRemove)
//            TouchpadButton.DPadButtonDragUp += action;
//        else
//            TouchpadButton.DPadButtonDragUp -= action;
//    }
//
//    /// <summary>
//    /// TouchpadButton 버튼이 터치된 상태에서 아래로 움직였을 떄 호출되는 함수
//    /// </summary>
//    public void OnTouchpadButtonDragDown(UnityAction action, bool addOrRemove)
//    {
//        if (addOrRemove)
//            TouchpadButton.DPadButtonDragDown += action;
//        else
//            TouchpadButton.DPadButtonDragDown -= action;
//    }
//
//    /// <summary>
//    /// TouchpadButton 버튼이 터치된 상태에서 왼쪽으로 움직였을 떄 호출되는 함수
//    /// </summary>
//    public void OnTouchpadButtonDragLeft(UnityAction action, bool addOrRemove)
//    {
//        if (addOrRemove)
//            TouchpadButton.DPadButtonDragLeft += action;
//        else
//            TouchpadButton.DPadButtonDragLeft -= action;
//    }
//
//    /// <summary>
//    /// TouchpadButton 버튼이 터치된 상태에서 오르쪽으로 움직였을 떄 호출되는 함수
//    /// </summary>
//    public void OnTouchpadButtonDragRight(UnityAction action, bool addOrRemove)
//    {
//        if (addOrRemove)
//            TouchpadButton.DPadButtonDragRight += action;
//        else
//            TouchpadButton.DPadButtonDragRight -= action;
//    }

    #endregion

    #endregion
}