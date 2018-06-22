using System;
using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;
using Valve.VR;

public class InventorySlotController : ItemSlotController
{
    #region outlets

    #endregion

    #region fields

    private InventorySlotController _interSlotController;
    private PlayerInventory _inventory;
    private InterSlotController _interSlot;
    private DeviceInteraction _rightController;

    public int Row { get; set; }
    public int Colum { get; set; }


    #endregion

    #region messages
	protected override void Awake ()
	{
        _interSlot = GameObject.Find("InterSlot").GetComponent<InterSlotController>();
	    _interSlotController = GameObject.Find("InterSlot").GetComponent<InventorySlotController>();
        _inventory = PlayerInventory.Instance;

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
                SwapItemToInterSlot();
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

    private void SwapItemToInterSlot()
    {
        GameItem temp = SlotItem;
        SlotItem = _interSlot.SlotItem;
        _interSlot.SlotItem = temp;

        _inventory.Items[Row, Colum] = SlotItem;

        _interSlot.SetSlotItem();
        SetSlotItem();
    }



    #endregion	

    #region methods



    #endregion
}
