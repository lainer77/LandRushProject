using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Units;
using UnityEngine;
using UnityScriptHelper;

public class DataSaverScript : MonoBehaviourEx
{
    private Player _player;
    private PlayerInventory _inventory;
    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        _player = Player.Instance;
        _inventory = PlayerInventory.Instance;
    }
}
