using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.PlayerItemManagers;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class StorageController : MonoBehaviourEx
{
    #region outlets
    
    #endregion

    #region fields

    public GameObject InventorySlot;
    public GameObject InterSlot;

    private ItemStorage _storage;
    private GridLayoutGroup _slotGroup;
    private InterSlotController _interSlot;
    private List<InventorySlotController> _slots;
    
    // Use this for initialization
    protected override void Awake()
    {

        _interSlot = InterSlot.GetComponent<InterSlotController>();

        _interSlot.SetSlotItem();

        _slots = new List<InventorySlotController>();

        _storage = new ItemStorage();

        DontDestroyOnLoad(this);

    }

    protected override void Update ()
	{
		
	}
    #endregion

    #region methods
    public void SetInventory()
    {
        _slotGroup = GetComponentInChildren<GridLayoutGroup>();

        for (int i = 0; i < _storage.Row; i++)
        {
            for (int j = 0; j < _storage.Column; j++)
            {
                GameObject slot = Instantiate(InventorySlot);
                InventorySlotController slotController = slot.GetComponent<InventorySlotController>();
                _slots.Add(slotController);

                slotController.SlotItem = _storage.Items[i, j];
                slotController.Row = i;
                slotController.Colum = j;

                slotController.SetSlotItem();

                slot.transform.SetParent(_slotGroup.transform, false);


            }
        }

    }
    #endregion
}
