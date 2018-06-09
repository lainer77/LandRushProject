using System.Collections;
using System.Security.Cryptography.X509Certificates;
using LandRushLibrary.ItemManagers;
using UnityEngine;
using UnityScriptHelper;
using Valve.VR;

public class CharacterControllerDevice : MonoBehaviourEx
{
    #region outlets

    public float MoveSpeed;

    #endregion

    #region fields

    private Transform _cameraTransform;
    private CharacterController _playerController;
    private GameObject _inventory;
    private InventoryController _inventoryController;
    private Rigidbody _rigidbody;
    private DeviceInteraction _leftController;
    private DeviceInteraction _rightController;
    private Vector3 _lookDirection;


    #endregion

    #region messages

    protected override void OnDestroy()
    {
        //ControllSetting(false);
    }


    protected override void Start()
    {
        _cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        _playerController = GetComponent<CharacterController>();
        _inventory = GameObject.FindWithTag("Inventory");
        _leftController = DeviceRepository.LeftDeviceInteraction;
        _rightController = DeviceRepository.RightDeviceInteraction;
        _rigidbody = GetCachedComponent<Rigidbody>();
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
        _rightController.SystemMenuButton.SetDeviceButtonDownEvent(VisableInventory, addOrRemove);
    }
    private void MoveUp()
    {
        Vector3 lookDirectionForwad = _cameraTransform.TransformDirection(Vector3.forward);
        _playerController.Move(lookDirectionForwad * MoveSpeed);

    }

    private void MoveDown()
    {
        Vector3 lookDirectionBack = _cameraTransform.TransformDirection(Vector3.back);
        _playerController.Move(lookDirectionBack * MoveSpeed);
    }
    private void MoveLeft()
    {
        Vector3 lookDirectionLeft = _cameraTransform.TransformDirection(Vector3.left);
        _playerController.Move(lookDirectionLeft * MoveSpeed);
    }
    private void MoveRight()
    {
        Vector3 lookDirectionRight = _cameraTransform.TransformDirection(Vector3.right);
        _playerController.Move(lookDirectionRight * MoveSpeed);
    }

    private void VisableInventory()
    {
        if (_inventory.activeSelf)
        {
            _inventory.SetActive(false);
        }
        else
            _inventory.SetActive(true);

    }

    #endregion
}