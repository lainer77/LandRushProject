using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class EquipmentWindowController : MonoBehaviourEx
{
    #region outlets

    #endregion

    #region fields

    private PlayerEquipment _playerEquipment;

    public GameObject currentLeftSlot;
    public GameObject currentRightSlot;
    public GameObject spareLeftSlot;
    public GameObject spareRightSlot;

    private EquipmentSlotController _currentLeft;
    private EquipmentSlotController _currentRight;
    private EquipmentSlotController _spareLeft;
    private EquipmentSlotController _spareRight;

    #endregion

    #region messages
	protected override void Awake ()
	{
	    _playerEquipment = Player.Instance.Equipment;
        InitEquipmentSlots();

	    Sword sword = ItemFactory.Instance.Create<Sword>(ItemID.OldSword);

        Player.Instance.Equipment.EquipItem(EquipmentSlot.Left, ItemFactory.Instance.Create<Shield>(ItemID.OldShield));
        Player.Instance.Equipment.EquipItem(EquipmentSlot.Right, sword );

        Player.Instance.Equipment.SparePair.LeftEquipment = ItemFactory.Instance.Create<Bow>(ItemID.OldBow);
        Player.Instance.Equipment.SparePair.RightEquipment = ItemFactory.Instance.Create<Arrow>(ItemID.Arrow);

        Player.Instance.Equipment.ChangeNextPair();
    }
	

    #endregion	

    #region methods

    private void InitEquipmentSlots()
    {
        _currentLeft = currentLeftSlot.GetComponent<EquipmentSlotController>();
        _currentRight = currentRightSlot.GetComponent<EquipmentSlotController>();
        _spareLeft = spareLeftSlot.GetComponent<EquipmentSlotController>();
        _spareRight = spareRightSlot.GetComponent<EquipmentSlotController>();

        _currentLeft.IsCurrent = true;
        _currentRight.IsCurrent = true;
        _spareLeft.IsCurrent = false;
        _spareRight.IsCurrent = false;

        _currentLeft.SlotItem = _playerEquipment.LeftEquipment;
        _currentRight.SlotItem = _playerEquipment.RightEquipment;
        _spareLeft.SlotItem = _playerEquipment.SparePair.LeftEquipment;
        _spareRight.SlotItem = _playerEquipment.SparePair.RightEquipment;

        _playerEquipment.EquipmentChanged += OnEquipmentChanged;
        _playerEquipment.CurrentPairChanged += OnPairChanged;
    }

    private void OnEquipmentChanged(object sender, PlayerEquipment.EquipmentChangedEventArgs e)
    {
        if (e.EquipmentSlot == EquipmentSlot.Left)
            _currentLeft.SlotItem = e.NewEquipment;
        else
            _currentRight.SlotItem = e.NewEquipment;
    }

    private void OnPairChanged(object sender, PlayerEquipment.CurrentPairChangedEventArgs e)
    {
        _currentLeft.SlotItem = e.NewPair.LeftEquipment;
        _currentRight.SlotItem = e.NewPair.RightEquipment;
        _spareLeft.SlotItem = e.PrevPair.LeftEquipment;
        _spareRight.SlotItem = e.PrevPair.RightEquipment;
    }

    #endregion
}
