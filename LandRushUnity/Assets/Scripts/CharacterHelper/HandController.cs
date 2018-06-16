using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class HandController : MonoBehaviourEx
{

    public GameObject Controller;

    private Animator _animator;
    private readonly int _hashIsTrigger = UnityEngine.Animator.StringToHash("IsTriggerDown");
    private DeviceInteraction _controller;

    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = Controller.GetComponent<DeviceInteraction>();
        _controller.TriggerButton.SetDeviceButtonDownEvent(SetBoolAniDown, true);
        _controller.TriggerButton.SetDeviceButtonUpEvent(SetBoolAniUp, true);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "STORAGE")
        {
            Debug.Log("TOUCH STORAGE");
        }

        if (other.tag == "ANVIL")
        {
            Debug.Log("TOUCH ANVIL");
        }
    }
    
    // Update is called once per frame

    void SetBoolAniDown()
    {
        _animator.SetBool(_hashIsTrigger, true);
    }
    void SetBoolAniUp()
    {
        _animator.SetBool(_hashIsTrigger, false);
    }
}
