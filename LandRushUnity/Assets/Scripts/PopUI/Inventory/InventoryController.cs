using System;
using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Repository;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class InventoryController : MonoBehaviourEx
{
    public GameObject InventorySlot;

    private PlayerInventory _inventory;
    private GridLayoutGroup _slotGroup;
    private InterSlotController _interSlot;
    private List<InventorySlotController> _slots;

    // Use this for initialization
    protected override void Start ()
	{
        _inventory = PlayerInventory.Instance;
	    _inventory.InventoryItemChanged += OnInvenItemChanged;
	    _interSlot = GameObject.Find("InterSlot").GetComponent<InterSlotController>();

        _interSlot.SetSlotItem();

        _slots = new List<InventorySlotController>();

        SetInventory();

	    ItemCreator.CreateItemObject<IngredientItem>(ItemID.Stone).transform.position = new Vector3(-2.0f, -2.0f, -10.0f);

        ItemCreator.CreateItemObject<Potion>(ItemID.HpPotion).transform.position = new Vector3(-4.0f, -2.0f, -10.0f);

	    ItemCreator.CreateItemObject<IngredientItem>(ItemID.Wood).transform.position = new Vector3(0.0f, -2.0f, -10.0f);

    }

    protected override void Update ()
	{

	}

    protected void OnInvenItemChanged(object sender, PlayerInventory.InventoryItemChangedEventArgs e)
    {
        int index = 0;

        foreach (var item in _inventory.Items)
        {
            _slots[index].SlotItem = item;
            _slots[index++].SetSlotItem();
        }
    }

    public void SetInventory()
    {
        _slotGroup = GetComponentInChildren<GridLayoutGroup>();

        for (int i = 0; i < _inventory.Row; i++)
        {
            for (int j = 0; j < _inventory.Column; j++)
            {
                GameObject slot = Instantiate(InventorySlot);
                InventorySlotController slotController = slot.GetComponent<InventorySlotController>();
                _slots.Add(slotController);

                slotController.SlotItem = _inventory.Items[i, j];
                slotController.Row = i;
                slotController.Colum = j;

                slotController.SetSlotItem();

                slot.transform.SetParent(_slotGroup.transform, false);


            }
        }

    }

    
}
