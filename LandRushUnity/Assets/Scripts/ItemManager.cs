using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

public class ItemManager : MonoBehaviourEx
{
    public ItemType ItemType;
    public Sprite DefaultImage;
    public int MaxCount;

    private SteamVR_Controller.Device _controller;
    private InventoryManager _inventory;

    protected override void Awake()
    {
        
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (_controller.GetHairTriggerDown() && other.gameObject.layer == 10)
            AddItem();
    }

    private void AddItem()
    {
        if (!_inventory.AddItem(this))
            Debug.Log("인벤토리가 가득찼습니다.");
        else
            gameObject.SetActive(false);
    }
}
