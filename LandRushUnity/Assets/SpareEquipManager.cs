using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using UnityEngine;
using UnityScriptHelper;

public class SpareEquipManager : MonoBehaviourEx
{
    #region outlets

    public GameObject RightController, LeftController;
    #endregion

    #region fields

    private DeviceInteraction _right, _left;
    private GameObject _sword, _shield, _bow, _arrow;
    #endregion

    #region messages
    protected override void Start()
    {
        _right = RightController.GetComponent<DeviceInteraction>();
        _left = LeftController.GetComponent<DeviceInteraction>();
        _sword = GameObject.FindWithTag("SWORD");
        _bow = GameObject.FindWithTag("Bow");
        _shield = GameObject.FindWithTag("SHIELD");
        _arrow = GameObject.FindWithTag("Arrow");
    }

    protected override void Update()
    {

    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other == RightController && _right.Controller.GetHairTriggerDown())
        {
            if (_sword.activeSelf)
            {
                SwapEquipment(_sword, _arrow);
            }
            else if (_arrow.activeSelf)
            {
                SwapEquipment(_arrow, _sword);
            }
        }

        if (other == LeftController && _left.Controller.GetHairTriggerDown())
        {
            if (_shield.activeSelf)
            {
                SwapEquipment(_shield,_bow);
            }
            else if (_bow.activeSelf)
            {
                SwapEquipment(_bow,_shield);
            }
            
        }
    }

    #endregion	

    #region methods

    private void SwapEquipment(GameObject usingEquip, GameObject spareEquip)
    {
        GameObject emptyObject;
        
        emptyObject = usingEquip;
        usingEquip = spareEquip;
        spareEquip = emptyObject;
        usingEquip.SetActive(true);
        spareEquip.SetActive(false);
        emptyObject = null;
    }

   
    #endregion
}
