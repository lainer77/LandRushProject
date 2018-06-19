using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class ItemSlotController : MonoBehaviourEx
{
    #region outlets

    #endregion

    #region fields


    protected Image _icon;
    public string IconPath = "Icons/";

    private GameItem _gameItem;
    public GameItem SlotItem
    {
        get { return _gameItem; }
        set
        {
            _gameItem = value;
            SetSlotItem();
        }
    }
    #endregion

    #region messages

    #endregion

    #region methods
    public void SetSlotItem()
    {
        Image[] images = GetComponentsInChildren<Image>();
        _icon = images[1];

        SetIconImage();
        SetItemAmountText();
    }

    protected void SetIconImage()
    {
        if (SlotItem != null)
        {
            _icon.sprite = Resources.Load<Sprite>(IconPath + SlotItem.IconName);
        }
        else
        {
            _icon.sprite = Resources.Load<Sprite>(IconPath + "SlotNull");
        }
    }

    protected void SetItemAmountText()
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
