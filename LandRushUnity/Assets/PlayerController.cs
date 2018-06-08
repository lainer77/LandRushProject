using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;
using Valve.VR;

public class PlayerController : MonoBehaviourEx
{
    #region outlets

    public float MoveSpeed;

    #endregion

    #region fields
    private GameObject _inventory;
    private SteamVR_TrackedObject _rightTrackedObject;
    private SteamVR_TrackedObject _leftTrackedObject;
    private CharacterController _characterController;
    private SteamVR_Controller.Device _rightDevice;

    private Transform _cameraTransform;
    
    #endregion

    #region messages
    protected override void Start ()
    {
        _rightTrackedObject = GameObject.FindWithTag("RightController").GetComponent<SteamVR_TrackedObject>();
        _leftTrackedObject = GameObject.FindWithTag("LeftController").GetComponent<SteamVR_TrackedObject>();
        _inventory = GameObject.FindWithTag("Inventory");
	    _characterController = GetComponent<CharacterController>();
        _cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();

	}
	
	protected override void Update ()
	{
	    _rightDevice = SteamVR_Controller.Input((int) _rightTrackedObject.index);

	    if (_rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
	    {
            InventoryVisable();
	    }
       
        
    }


    #endregion

    #region methods

    public void InventoryVisable()
    {
        if (_inventory.activeSelf)
            _inventory.SetActive(false);
        else
            _inventory.SetActive(true);
    }

    public void MoveFoward()
    {
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.forward);
        _characterController.SimpleMove(lookDirection * MoveSpeed);
    }
    public void MoveBack()
    {
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.back);
        _characterController.SimpleMove(lookDirection * MoveSpeed);
    }
    public void MoveLeft()
    {
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.left);
        _characterController.SimpleMove(lookDirection * MoveSpeed);
    }
    public void MoveRight()
    {
        Vector3 lookDirection = _cameraTransform.TransformDirection(Vector3.right);
        _characterController.SimpleMove(lookDirection * MoveSpeed);
    }
    #endregion


}
