using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Factory;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;
using LandRushLibrary.ItemManagers;
using LandRushLibrary.Items;

public class ItemManager : MonoBehaviourEx
{
    public ItemType ItemType;
    public ItemID ItemId;
    public int MaxCount;

    private GameItem _gameItem;
    protected override void Awake()
    {
    }
}
