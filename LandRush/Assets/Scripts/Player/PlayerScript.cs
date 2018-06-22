using System;
using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(EquipedWait());
    }

    IEnumerator EquipedWait()
    {
        var equipedWait = new List<object>();
        while (true)
        {
            if (ItemScriptRepository.ItemDictionary.Count <= 0)
            {
                equipedWait.Add(null);
                continue;
            }
            try
            {
                Player.Equipment.EquipItem(EquipmentSlot.Left, ItemScriptRepository.ItemDictionary[ItemID.OldShield].Item);
                Player.Equipment.EquipItem(EquipmentSlot.Right, ItemScriptRepository.ItemDictionary[ItemID.OldSword].Item);
                Player.Equipment.EquipmentPairs[1].LeftEquipment = ItemScriptRepository.ItemDictionary[ItemID.OldBow].Item;
                Player.Equipment.EquipmentPairs[1].RightEquipment = ItemScriptRepository.ItemDictionary[ItemID.Arrow].Item;
                break;
            }
            catch (Exception e)
            {
                equipedWait.Add(null);
            }
        }

        return equipedWait.GetEnumerator();
    }
    private void OnEquipItemChanged(object sender, PlayerEquipment.EquipmentChangedEventArgs e)
    {
        (e.NewEquipment.GameTag as GameObject)?.SetActive(true);
        (e.PrevEquipment.GameTag as GameObject)?.SetActive(false);
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
        (e.NewPair.LeftEquipment.GameTag as GameObject)?.SetActive(true);
        (e.NewPair.RightEquipment.GameTag as GameObject)?.SetActive(true);
        (e.PrevPair.LeftEquipment.GameTag as GameObject)?.SetActive(false);
        (e.PrevPair.RightEquipment.GameTag as GameObject)?.SetActive(false);
    }
}