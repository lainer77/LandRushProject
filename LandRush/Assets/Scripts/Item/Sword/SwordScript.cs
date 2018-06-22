using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

// ReSharper disable once CheckNamespace
public class SwordScript : EquipmentItemScript
{
    public ItemID ItemId = ItemID.OldSword;
    protected override ItemID GetInstanceItemId()
    {
        return ItemId;
    }
}