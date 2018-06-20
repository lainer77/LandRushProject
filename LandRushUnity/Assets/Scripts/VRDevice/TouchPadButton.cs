using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityScriptHelper;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TouchPadButton : DeviceButton
{
    public TouchPadButton(EVRButtonId deviceButtonId) : base(deviceButtonId)
    {
    }

    protected bool TouchIn
    {
        set
        {
            if (value)
                OnDPadButtonIn();
            _touchIn = value;
        }
    }

    protected bool TouchOut
    {
        set
        {
            if (value)
                OnDPadButtonOut();
            _touchOut = value;
        }
    }

    protected bool TouchDrag
    {
        set
        {
            if (value)
                OnDPadButtonDrag();
            _touchDrag = value;
        }
    }

    private bool _touchIn;
    private bool _touchOut;
    private bool _touchDrag;


    private SteamVR_Controller.Device _controller;
    private const float Threshold = 0.45f;
    private EVRButtonId _dPadButtonId;

    public override void OnPress(SteamVR_Controller.Device controller)
    {
        _controller = controller;
        TouchIn = controller.GetTouchDown(_deviceButtonId);
        TouchOut = controller.GetTouchUp(_deviceButtonId);
        TouchDrag = controller.GetTouch(_deviceButtonId);
        base.OnPress(controller);
    }

    #region DPadDrag

    protected event UnityAction DPadButtonIn;

    protected virtual void OnDPadButtonIn()
    {
        Vector2 touchpadAxis = _controller.GetAxis(_deviceButtonId);
        if (touchpadAxis.y > (1.0f - Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Up;
        }
        else if (touchpadAxis.y < (-1f + Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Down;
        }
        else if (touchpadAxis.x > (1.0f - Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Right;
        }
        else if (touchpadAxis.x < (-1f + Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Left;
        }

        DPadButtonIn?.Invoke();
    }

    protected event UnityAction DPadButtonOut;

    protected virtual void OnDPadButtonOut()
    {
        _dPadButtonId = EVRButtonId.k_EButton_Axis0;
        DPadButtonOut?.Invoke();
    }

    protected event UnityAction DPadButtonDrag;

    protected virtual void OnDPadButtonDrag()
    {
        Vector2 touchpadAxis = _controller.GetAxis(_deviceButtonId);

        if (touchpadAxis.y > (1.0f - Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Up)
            {
                OnDPadButtonDragUp();
            }
        }
        else if (touchpadAxis.y < (-1f + Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Down)
            {
                OnDPadButtonDragDown();
            }
        }
        else if (touchpadAxis.x > (1.0f - Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Right)
            {
                OnDPadButtonDragRight();
            }
        }
        else if (touchpadAxis.x < (-1f + Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Left)
            {
                OnDPadButtonDragLeft();
            }
        }

        DPadButtonDrag?.Invoke();
    }

    protected event UnityAction DPadButtonDragUp;

    protected virtual void OnDPadButtonDragUp()
    {
        DPadButtonDragUp?.Invoke();
    }

    protected event UnityAction DPadButtonDragDown;

    protected virtual void OnDPadButtonDragDown()
    {
        DPadButtonDragDown?.Invoke();
    }

    protected event UnityAction DPadButtonDragRight;

    protected virtual void OnDPadButtonDragRight()
    {
        DPadButtonDragRight?.Invoke();
    }

    protected event UnityAction DPadButtonDragLeft;

    protected virtual void OnDPadButtonDragLeft()
    {
        DPadButtonDragLeft?.Invoke();
    }

    #endregion

    #region DPadControll

    protected event UnityAction DPadUpButton;
    protected event UnityAction DPadDownButton;
    protected event UnityAction DPadRightButton;
    protected event UnityAction DPadLeftButton;

    protected virtual void OnDPadUpButton()
    {
        DPadUpButton?.Invoke();
    }

    protected virtual void OnDPadDownButton()
    {
        DPadDownButton?.Invoke();
    }

    protected virtual void OnDPadRightButton()
    {
        DPadRightButton?.Invoke();
    }

    protected virtual void OnDPadLeftButton()
    {
        DPadLeftButton?.Invoke();
    }

    public virtual void SetDPadUpButtonEvent(UnityAction action, bool addOrRemove)
    {
        if (addOrRemove)
            DPadUpButton += action;
        else
            DPadUpButton -= action;
    }

    public virtual void SetDPadDownButtonEvent(UnityAction action, bool addOrRemove)
    {
        if (addOrRemove)
            DPadDownButton += action;
        else
            DPadDownButton -= action;
    }

    public virtual void SetDPadRightButtonEvent(UnityAction action, bool addOrRemove)
    {
        if (addOrRemove)
            DPadRightButton += action;
        else
            DPadRightButton -= action;
    }

    public virtual void SetDPadLeftButtonEvent(UnityAction action, bool addOrRemove)
    {
        if (addOrRemove)
            DPadLeftButton += action;
        else
            DPadLeftButton -= action;
    }

    #endregion

    protected UnityAction MoveAction;
    protected override void OnDeviceButtonUp()
    {
        MoveAction?.Invoke();
    }

    protected override void OnDeviceButtonDown()
    {
        Vector2 touchpadAxis = _controller.GetAxis(_deviceButtonId);

        if (touchpadAxis.y > (1.0f - Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Up;
        }
        else if (touchpadAxis.y < (-1f + Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Down;
        }
        else if (touchpadAxis.x > (1.0f - Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Right;
        }
        else if (touchpadAxis.x < (-1f + Threshold))
        {
            _dPadButtonId = EVRButtonId.k_EButton_DPad_Left;
        }
    }

    protected override void OnDeviceButtonPress()
    {
        Vector2 touchpadAxis = _controller.GetAxis(_deviceButtonId);

        if (touchpadAxis.y > (1.0f - Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Up)
            {
                MoveAction = OnDPadUpButton;
                MoveAction();
                return;
            }
        }
        else if (touchpadAxis.y < (-1f + Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Down)
            {
                MoveAction = OnDPadDownButton;
                MoveAction();
                return;
            }
        }
        else if (touchpadAxis.x > (1.0f - Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Right)
            {
                MoveAction = OnDPadRightButton;
                MoveAction();
                return;
            }
        }
        else if (touchpadAxis.x < (-1f + Threshold))
        {
            if (_dPadButtonId == EVRButtonId.k_EButton_DPad_Left)
            {
                MoveAction = OnDPadLeftButton;
                MoveAction();
                return;
            }
        }

        base.OnDeviceButtonPress();
    }

}