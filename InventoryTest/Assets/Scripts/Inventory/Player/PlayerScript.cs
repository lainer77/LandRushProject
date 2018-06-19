using System;
using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using UnityEngine;
using UnityScriptHelper;

// ReSharper disable once CheckNamespace
public class PlayerScript : MonoBehaviourEx
{
    private Player _player;

    public Player Player
    {
        get
        {
            if (_player == null)
                _player = Player.Instance;
            return _player;
        }
    }

    public ShoulderScript Shoulder;

    protected override void Start()
    {
        Player.Equipment.EquipmentChanged += OnEquipItemChanged;
        Player.Equipment.CurrentPairChanged += OnPairChanged;

        Player.Equipment.EquipItem(EquipmentSlot.Left, ItemScriptRepository.ItemDictionary[ItemID.OldSword].Item);
        Player.Equipment.EquipItem(EquipmentSlot.Right, ItemScriptRepository.ItemDictionary[ItemID.OldSword].Item);
        Player.Equipment.EquipmentPairs[1].LeftEquipment = ItemScriptRepository.ItemDictionary[ItemID.OldBow].Item;
        Player.Equipment.EquipmentPairs[1].RightEquipment = ItemScriptRepository.ItemDictionary[ItemID.Arrow].Item;
    }

    private void OnEquipItemChanged(object sender, PlayerEquipment.EquipmentChangedEventArgs e)
    {
        GameObject o = ItemScriptRepository.ItemDictionary[e.NewEquipment.ItemId].gameObject;
        if (o != null)
            o.SetActive(true);
        o = ItemScriptRepository.ItemDictionary[e.PrevEquipment.ItemId].gameObject;
        if (o != null)
            o.SetActive(false);
    }
        
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Shoulder)
        {
            Player.Equipment.ChangeNextPair();
        }
    }

    private void OnPairChanged(object sender, PlayerEquipment.CurrentPairChangedEventArgs e)
    {
        GameObject o = ItemScriptRepository.ItemDictionary[e.NewPair.LeftEquipment.ItemId].gameObject;
        GameObject o2 = ItemScriptRepository.ItemDictionary[e.NewPair.RightEquipment.ItemId].gameObject;
        if (o != null)
            o.SetActive(true);
        if (o2 != null)
            o2.SetActive(true);
        o = ItemScriptRepository.ItemDictionary[e.PrevPair.LeftEquipment.ItemId].gameObject;
        o2 = ItemScriptRepository.ItemDictionary[e.PrevPair.RightEquipment.ItemId].gameObject;
        if (o != null)
            o.SetActive(false);
        if (o2 != null)
            o2.SetActive(false);
    }
}