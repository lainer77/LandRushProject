using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityScriptHelper;

public class CharacterControllerDevice : MonoBehaviourEx
{
    #region outlets

    public float MoveSpeed;
    #endregion

    #region fields

    private Transform _camTransform;

    #endregion

    #region messages

    protected override void OnDestroy()
    {
        ControllSetting(false);
    }

    private DeviceInteraction _leftController;
    private RightDeviceInteraction _rightController;

    protected override void Start()
    {
        _leftController = DeviceRepository.LeftDeviceInteraction;
        _rightController = DeviceRepository.RightDeviceInteraction;
        _camTransform = Camera.main.GetComponent<Transform>();
        ControllSetting(true);
    }

    #endregion

    #region methods

    public void ControllSetting(bool addOrRemove)
    {
        _leftController.TouchpadButton.SetDPadUpButtonEvent(MoveUp, addOrRemove);
        _leftController.TouchpadButton.SetDPadDownButtonEvent(MoveDown, addOrRemove);
        _leftController.TouchpadButton.SetDPadLeftButtonEvent(MoveLeft, addOrRemove);
        _leftController.TouchpadButton.SetDPadRightButtonEvent(MoveRight, addOrRemove);
    }

    private void MoveUp()
    {
        MoveTo(Vector3.forward);
    }

    private void MoveDown()
    {
        MoveTo(Vector3.back);
    }

    private void MoveLeft()
    {
        MoveTo(Vector3.left);
    }

    private void MoveRight()
    {
        MoveTo(Vector3.right);
    }

    public void MoveTo(Vector3 vector)
    {
        Vector3 dir = _camTransform.TransformDirection(vector);
        transform.Translate(dir * Time.deltaTime * MoveSpeed);
    }

    #endregion
}