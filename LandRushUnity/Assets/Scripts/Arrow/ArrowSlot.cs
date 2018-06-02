using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;
using Valve.VR.InteractionSystem;

public class ArrowSlot : MonoBehaviourEx {

	// Use this for initialization

    private RightDeviceInteraction _rightDeviceInteraction;
    private IInventoriable _inventoriable;
    private Inventory _inventory;
    public Arrow CurrentArrow { get; set; }
    protected override void OnCollisionEnter(Collision collision)
    {
        GameObject ob = collision.gameObject;
        if (ob.tag == "ArrowSlot")
        {
//            _inventoriable = collision.transform.GetComponent<IInventoriable>();
//            _inventory = _inventoriable.GetInventory();
            CurrentArrow = _inventory.PopArrow();
            CurrentArrow.transform.parent = _rightDeviceInteraction.transform;
            CurrentArrow.transform.localPosition = Vector3.zero;
            CurrentArrow.transform.rotation = _rightDeviceInteraction.transform.rotation;
        }
    }
}

public interface IInventoriable
{
    void OpenInventory();
    Inventory GetInventory();
}

public class Inventory
{
    public Arrow PopArrow()
    {
        return new Arrow();
    }
}