using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Factory;
using UnityEngine;
using UnityScriptHelper;
using LandRushLibrary.ItemManagers;
using LandRushLibrary.Repository;

public class InventoryController : MonoBehaviourEx
{
    #region outlets

    public List<GameObject> AllSlot; // 모든 슬롯 저장 리스트
    public RectTransform InventoryRectTransform; // 인벤토리 RectTransform
    public GameObject OriginalSlot; // 오리지날 슬롯 프리팹

    public float SlotSize; //슬롯의 size
    public float SlotGap; //슬롯 사이간격
    public bool IsVisable;
    public float SlotCountHor; // 가로 슬롯수
    public float SlotCountVer; // 세로 슬롯수
    #endregion

    #region fields

    private float _inventoryWidth; //인벤토리 가로 길이
    private float _inventoryHeight; // 인벤토리 세로길이
    private float _emptySlot; //빈슬롯의 수
    private InventoryManager _inventoryManager;

    #endregion

    #region messages

    protected override void Awake()
    {
        _inventoryManager = InventoryManager.Instance;
        CreateInventory();
        AddItem();  
        SetSlotItem();

    }

    #endregion	

    #region methods
    void CreateInventory()
    {
        _inventoryWidth = (SlotCountHor * SlotSize) + (SlotCountHor * SlotGap) + SlotGap + 2;
        _inventoryHeight = (SlotCountVer * SlotSize) + (SlotCountVer * SlotGap) + SlotGap + 4;
        // 인벤토리 가로, 세로 길이 설정
        InventoryRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _inventoryWidth);
        InventoryRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _inventoryHeight);

        for (int y = 0; y < SlotCountVer; y++)
        {
            for (int x = 0; x < SlotCountHor; x++)
            {
                GameObject slot = Instantiate(OriginalSlot);

                RectTransform slotRectTransform = slot.GetComponent<RectTransform>();

                RectTransform itemRectTransform = slot.transform.GetChild(0).GetComponent<RectTransform>();

                slot.name = "slot_" + y + "_" + x;
                slot.transform.parent = transform;

                slotRectTransform.localPosition = new Vector3(-((SlotSize * x) + (SlotGap * (x - 4f))),
                    -((SlotSize * y) + (SlotGap * (y - 5.2f))), 0);

                slotRectTransform.localScale = Vector3.one;
                slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
                slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);

                itemRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize * 0.9f);
                itemRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize * 0.9f);

                AllSlot.Add(slot);
            }
        }

        _emptySlot = AllSlot.Count;
    }

    void AddItem()
    {
        _inventoryManager.AddInvenItem(ItemFactory.Instance.Create(ItemID.POTION));
    }

    private void SetSlotItem()
    {
        for (int i = 0; i < _inventoryManager.Items.Count; i++)
        {
            AllSlot[i].GetComponent<InventorySlot>().SetItem(_inventoryManager.Items[i].Item);
        }
    }

    #endregion
}
