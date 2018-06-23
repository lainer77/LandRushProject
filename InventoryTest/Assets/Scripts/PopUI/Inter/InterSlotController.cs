using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class InterSlotController : ItemSlotController
{
    #region outlets

    #endregion

    #region fields
    private DeviceInteraction _rightController;
    #endregion

    #region messages
    protected override void Awake()
    {
        _rightController = DeviceRepository.RightDeviceInteraction;
    }
	
	protected override void Update ()
	{
	    RaserCraft laser = _rightController.GetComponentInChildren<RaserCraft>();

	    if (laser.Hit.transform == null)
	        return;

	    if (laser.Hit.transform.gameObject == gameObject)
	    {
            Color color;
	        color = Color.green;

	        if (_icon.sprite == null)
	            color.a = 0.1f;
	        else
	            color.a = 1.0f;

	        _icon.color = color;

	        if (_rightController.Controller.GetHairTriggerDown())
	        {
                GameObject dropItem = ItemCreator.CreateItemObject(SlotItem);
	            dropItem.transform.position = GameObject.Find("[CameraRig]").transform.position;
	            SlotItem = null;
	        }
	    }
	    else
	    {
	        Color color;
	        color = Color.white;

	        if (_icon.sprite == null)
	            color.a = 0.0f;
	        else
	            color.a = 1.0f;

	        _icon.color = color;
	    }
    }
    #endregion	

    #region methods
    
    #endregion
}
