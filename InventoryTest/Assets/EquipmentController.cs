using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Units;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class EquipmentController : MonoBehaviourEx
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
	}
	

    #endregion	

    #region methods

    private void InitEquipmentSlots()
    {
        _currentLeft = currentLeftSlot.GetComponent<EquipmentSlotController>();
        _currentRight = currentRightSlot.GetComponent<EquipmentSlotController>();
        _spareLeft = spareLeftSlot.GetComponent<EquipmentSlotController>();
        _spareRight = spareLeftSlot.GetComponent<EquipmentSlotController>();

        _currentLeft.IsCurrent = true;
        _currentRight.IsCurrent = true;
        _spareLeft.IsCurrent = false;
        _spareRight.IsCurrent = false;

        _currentLeft.SlotItem = _playerEquipment.LeftEquipment;
        _currentRight.SlotItem = _playerEquipment.RightEquipment;
        _spareLeft.SlotItem = _playerEquipment.SparePair.LeftEquipment;
        _currentRight.SlotItem = _playerEquipment.SparePair.RightEquipment;

        _playerEquipment.EquipmentChanged += OnEquipmentChanged;
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
        _spareLeft.SlotItem = e.PrevPair.RightEquipment;
    }

    #endregion
}
