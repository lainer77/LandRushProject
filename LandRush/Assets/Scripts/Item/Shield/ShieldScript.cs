using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

// ReSharper disable once CheckNamespace
public class ShieldScript : EquipmentItemScript
{
    protected override ItemID GetInstanceItemId()
    {
        return ItemID.OldShield;
    }
}