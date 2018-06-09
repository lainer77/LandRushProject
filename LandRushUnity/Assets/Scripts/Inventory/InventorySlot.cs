using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class InventorySlot : MonoBehaviourEx
{
    #region outlet

    public Stack<GameItem> Slot;
    public Text Text;
    public Sprite DefaultImage;
    
    
    #endregion
    
    private GameItem _item;
    private Image _itemImage;
    private bool _isSlotEmpty;
    #region feild



    #endregion

    #region message
    protected override void Awake()
    {
        _itemImage = transform.GetChild(0).GetComponent<Image>();
    }


    #endregion

    #region method


    
    public void SetItem(GameItem item)
    {
        Slot.Push(item);
        ChangeImage(true, item);
    }

    public void ChangeImage(bool isSlotEmpty, GameItem item)
    {
        _itemImage.sprite = Resources.Load<Sprite>("Sprites/" + item.IconName);
    }

    #endregion

}
