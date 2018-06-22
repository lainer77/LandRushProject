using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using UnityEngine;
using UnityScriptHelper;

// ReSharper disable once CheckNamespace
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
        if (collision.transform.CompareTag(tag: Tags.RightController))
        {
            GameObject go = _rightDeviceInteraction.transform.Find(name: "Hand").gameObject;
            if (go.GetComponentsInChildren<ArrowScript>().Length == 1)
                return;

            GameObject arrow =
                ObjectPool.Instance.PopFromPool(itemName: Tags.Arrow, parent: DeviceRepository.RightDeviceInteraction.transform);

            if (arrow != null)
            {
                arrow.transform.localPosition = Vector3.zero;
                arrow.name = "Arrow";
            }
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