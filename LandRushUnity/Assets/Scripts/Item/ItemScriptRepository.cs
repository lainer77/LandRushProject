using System.Collections;
using System.Collections.Generic;

using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class ItemScriptRepository{
    public static Dictionary<ItemID, EquipmentItemScript> ItemDictionary { get; } = new Dictionary<ItemID, EquipmentItemScript>();
}
