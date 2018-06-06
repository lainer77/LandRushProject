using System.Collections;
using System.Security.Cryptography.X509Certificates;
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
    private InventoryManager _inventoryManager;
    private Rigidbody _rigidbody;
    private DeviceInteraction _leftController;
    private DeviceInteraction _rightController;
    #endregion

    #region messages

    protected override void OnDestroy()
    {
        ControllSetting(false);
    }


    protected override void Start()
    {
        _cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        _playerController = GetComponent<CharacterController>();
        _inventory = GameObject.FindWithTag("Inventory");
        _inventoryManager = _inventory.GetComponent<InventoryManager>();
        _leftController = DeviceRepository.LeftDeviceInteraction;
        _rightController = DeviceRepository.RightDeviceInteraction;
        _rigidbody = GetCachedComponent<Rigidbody>();
        ControllSetting(true);
    }

    private void Update()
    {

    }

    private void VisableInventory()
    {
        if (_inventoryManager.IsVisable == false)
        {
            _inventory.SetActive(true);
            _inventoryManager.IsVisable = true;
        }
        else
        {
            _inventory.SetActive(false);
            _inventoryManager.IsVisable = false;
        }

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
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.forward);
        _playerController.Move(lookDirection * MoveSpeed);
        
    }

    private void MoveDown()
    {
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.forward);
        _playerController.Move(lookDirection * MoveSpeed);
    }
    private void MoveLeft()
    {
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.forward);
        _playerController.Move(lookDirection * MoveSpeed);
    }
    private void MoveRight()
    {
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.forward);
        _playerController.Move(lookDirection * MoveSpeed);
    }



    #endregion
}