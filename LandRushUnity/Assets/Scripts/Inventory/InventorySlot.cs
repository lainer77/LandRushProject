using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class InventorySlot : MonoBehaviourEx
{
    public Stack<ItemManager> SlotStack;
    public Text Text;
    public Sprite DefaultImage;

    private Image _itemImage;
    private bool _isSlotEmpty;
    protected override void Start()
    {
        SlotStack = new Stack<ItemManager>();

        _isSlotEmpty = false;

        float size = Text.gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        Text.fontSize = (int) (size * 0.5f);

        _itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    public ItemManager ReturnItem()
    {
        return SlotStack.Peek();
    }

    public bool IsItemCountMax(ItemManager item)
    {
        return ReturnItem().MaxCount > SlotStack.Count;
    }

    public bool IsSlotEmptyFlag => _isSlotEmpty;

    public void SetSlots(bool b)
    {
        b = _isSlotEmpty;
    }

    public void AddItem(ItemManager item)
    {
        SlotStack.Push(item);
        UpdateInfo(true, item.DefaultImage);
    }

    public void ItemUse()
    {
        if(!_isSlotEmpty)
            return;

        if (SlotStack.Count == 1)
        {
            SlotStack.Clear();
            UpdateInfo(false, DefaultImage);
            return;
        }

        SlotStack.Pop();
        UpdateInfo(_isSlotEmpty, _itemImage.sprite);

    }

    public void UpdateInfo(bool b, Sprite image)
    {
        SetSlots(b);
        _itemImage.sprite = image;
        Text.text = SlotStack.Count > 1 ? SlotStack.Count.ToString() : "";
    }
}
