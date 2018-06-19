using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using UnityEngine;
using UnityScriptHelper;

public class Quiver : MonoBehaviourEx
{
    // Use this for initialization

    private DeviceInteraction _rightDeviceInteraction;
    private IInventoriable _inventoriable;
    private Inventory _inventory;
    public Arrow CurrentArrow { get; set; }
    protected override void Start()
    {
        _rightDeviceInteraction = DeviceRepository.RightDeviceInteraction;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Tags.RightController))
        {
            //            _inventoriable = collision.transform.GetComponent<IInventoriable>();
            //            _inventory = _inventoriable.GetInventory();
            //            CurrentArrow = _inventory.PopArrow();
            GameObject go = _rightDeviceInteraction.transform.Find("Hand").gameObject;
            if (go.GetComponentsInChildren<ArrowScript>().Length == 1)
                return;
            GameObject arrow = Instantiate(Resources.Load("Arrow"), _rightDeviceInteraction.transform.Find("Hand")) as GameObject;
            if (arrow != null) arrow.transform.localPosition = Vector3.zero;
            arrow.name = "Arrow";
//            CurrentArrow.transform.parent = _rightDeviceInteraction.transform;
//            CurrentArrow.transform.localPosition = Vector3.zero;
//            CurrentArrow.transform.rotation = _rightDeviceInteraction.transform.rotation;
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