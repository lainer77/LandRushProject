using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityScriptHelper;

public class CharacterControllerDevice : MonoBehaviourEx
{
    #region outlets
    #endregion

    #region fields

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
        _inventory = GameObject.FindWithTag("Inventory");
        _inventoryManager = _inventory.GetComponent<InventoryManager>();
        _leftController = DeviceRepository.LeftDeviceInteraction;
        _rightController = DeviceRepository.RightDeviceInteraction;
        _rigidbody = GetCachedComponent<Rigidbody>();
        ControllSetting(true);
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
        transform.Translate(vector);
    }

    #endregion
}