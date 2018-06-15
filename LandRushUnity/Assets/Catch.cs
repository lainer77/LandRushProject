using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class Catch : MonoBehaviourEx
{
    private DeviceInteraction _leftController;
    private Animator animator;
    protected override void Start()
    {
        _leftController = DeviceRepository.LeftDeviceInteraction;
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (_leftController.Controller.GetHairTriggerDown())
        {
            animator.SetTrigger("catch");
        }
       
    }
}