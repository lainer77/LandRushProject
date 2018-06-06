using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class InventoryManager : MonoBehaviourEx
{
    #region outlets

    public List<GameObject> AllSlot; // 모든 슬롯 저장 리스트
    public RectTransform InventoryRectTransform; // 인벤토리 RectTransform
    public GameObject OriginalSlot; // 오리지날 슬롯 프리팹

    public float SlotSize; //슬롯의 size
    public float SlotGap; //슬롯 사이간격

    public float SlotCountHor; // 가로 슬롯수
    public float SlotCountVer; // 세로 슬롯수
    #endregion

    #region fields
    
    private float _inventoryWidth; //인벤토리 가로 길이
    private float _inventoryHeight; // 인벤토리 세로길이
    private float _emptySlot; //빈슬롯의 수
    private bool _isVisable = false;
    #endregion

    #region messages

    protected override void Awake()
    {
        
        //인벤토리 가로, 세로 사이즈 설정
        _inventoryWidth = (SlotCountHor * SlotSize) + (SlotCountHor * SlotGap) + SlotGap+2;
        _inventoryHeight = (SlotCountVer * SlotSize) + (SlotCountVer * SlotGap) + SlotGap +4;
        // 인벤토리 가로, 세로 길이 설정
        InventoryRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _inventoryWidth);
        InventoryRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _inventoryHeight);

        for (int y = 0; y < SlotCountVer; y++)
        {
            for (int x = 0; x < SlotCountHor; x++)
            {
                GameObject slot = Instantiate(OriginalSlot) as GameObject;

                RectTransform slotRectTransform = slot.GetComponent<RectTransform>();

                RectTransform itemRectTransform = slot.transform.GetChild(0).GetComponent<RectTransform>();

                slot.name = "slot_" + y + "_" + x;
                slot.transform.parent = transform;

                slotRectTransform.localPosition = new Vector3(-((SlotSize* x) + (SlotGap *(x-4))),-((SlotSize * y) + (SlotGap *(y-5f))),0);

                slotRectTransform.localScale = Vector3.one;
                slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
                slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);

                itemRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize *0.9f);
                itemRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize *0.9f);

                AllSlot.Add(slot);
            }
        }

        _emptySlot = AllSlot.Count;

    }

    protected override void Update()
    {
    }

    #endregion	

    #region methods
    
    #endregion
}
