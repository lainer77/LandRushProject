using System;
using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;
using Valve.VR;

public class InventorySlotController : MonoBehaviourEx
{
    #region outlets

    #endregion

    #region fields

    private Image _icon;
    private InventorySlotController _interSlotController;
    private PlayerInventory _inventory;
    private InventorySlotController _interSlot;
    private DeviceInteraction _rightController;

    public int Row { get; set; }
    public int Colum { get; set; }
    public string IconPath = "Icons/";

    private GameItem _gameItem;
    public GameItem SlotItem
    {
        get { return _gameItem; }
        set { _gameItem = value; }
    }

    #endregion

    #region messages
	protected override void Awake ()
	{
        _interSlot = GameObject.Find("InterSlot").GetComponent<InventorySlotController>();
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
	        _icon.color = Color.green;

            if (_rightController.Controller.GetHairTriggerDown())
	        {
                SwapItemToInterSlot();
	        }
	    }
	    else
	    {
	        _icon.color = Color.white;
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

    protected override void OnMouseEnter()
    {
    }

    protected override void OnMouseExit()
    {
    }


    #endregion	

    #region methods




    public void SetSlotItem()
    {
        Image[] images = GetComponentsInChildren<Image>();
        _icon = images[1];

        SetIconImage();
        SetItemAmountText();
    }

    private void SetIconImage()
    {
        if (SlotItem != null)
        {
            _icon.sprite = Resources.Load<Sprite>(IconPath + SlotItem.IconName);
        }
        else
        {
            _icon.sprite = null;
        }
    }

    private void SetItemAmountText()
    {
        Text text = GetComponentInChildren<Text>();

        if (SlotItem != null)
        {
            if (SlotItem.Amount <= 1)
                text.text = "";
            else
                text.text = SlotItem.Amount.ToString();
        }
        else
        {
            text.text = "";
        }
    }

    #endregion
}
