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
    private InventorySlotController _interSlot;
    private List<InventorySlotController> _slots;

    // Use this for initialization
    protected override void Start ()
	{
        _inventory = PlayerInventory.Instance;
	    _inventory.InventoryItemChanged += OnInvenItemChanged;
	    _interSlot = GameObject.Find("InterSlot").GetComponent<InventorySlotController>();

        _interSlot.SetSlotItem();

        _slots = new List<InventorySlotController>();

        SetInventory();

	    //Potion potion = ItemFactory.Instance.Create<Potion>(ItemID.HpPotion);
	    //potion.Amount = 10;
	    //_inventory.AddGameItem(potion);

	    //IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.Stone);
	    //stone.Amount = 4;
	    //_inventory.AddGameItem(stone);

	    //Sword dragonSwrod = ItemFactory.Instance.Create<Sword>(ItemID.DragonSword);
     //   _inventory.AddGameItem(dragonSwrod);
	    //Console.WriteLine(_inventory.Items[0,2].Name);

	    //Sword oldSword = ItemFactory.Instance.Create<Sword>(ItemID.OldSword);
	    //_inventory.AddGameItem(oldSword);

	    ItemCreator.GetItemObject<IngredientItem>(ItemID.Stone).transform.position = new Vector3(-2.0f, -2.0f, -10.0f);

        ItemCreator.GetItemObject<Potion>(ItemID.HpPotion).transform.position = new Vector3(-4.0f, -2.0f, -10.0f);

	    ItemCreator.GetItemObject<IngredientItem>(ItemID.Wood).transform.position = new Vector3(0.0f, -2.0f, -10.0f);

    }


    // Update is called once per frame
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
