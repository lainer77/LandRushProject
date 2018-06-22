using System;
using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

// ReSharper disable once CheckNamespace
public abstract class EquipmentItemScript : MonoBehaviourEx
{
    protected override void Awake()
    {
        Debug.Log(GetInstanceItemId().ToString());
        ItemScriptRepository.ItemDictionary.Add(Item.ItemId, this);
        Item.GameTag = gameObject;
    }


    protected abstract ItemID GetInstanceItemId();
    private EquipmentItem _item;

    public EquipmentItem Item
    {
        get
        {
            if (_item == null)
                _item = ItemFactory.Instance.Create<EquipmentItem>(GetInstanceItemId());
            return _item;
        }
    }
}