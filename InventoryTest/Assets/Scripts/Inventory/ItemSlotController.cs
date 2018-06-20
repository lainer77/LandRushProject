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
        if (_gameItem != null)
        {
            _icon.sprite = Resources.Load<Sprite>(IconPath + _gameItem.IconName);
        }
        else
        {
            _icon.sprite = null;

            Color color = _icon.color;
            color.a = 0.0f;
            _icon.color = color;
        }
    }

    protected void SetItemAmountText()
    {
        Text text = GetComponentInChildren<Text>();

        if (_gameItem != null)
        {
            if (_gameItem.Amount <= 1)
                text.text = "";
            else
                text.text = _gameItem.Amount.ToString();
        }
        else
        {
            text.text = "";
        }
    }
    #endregion
}
