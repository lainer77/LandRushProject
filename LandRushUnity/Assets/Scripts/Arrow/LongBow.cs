﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityScriptHelper;
using Valve.VR.InteractionSystem;

public class LongBow : MonoBehaviourEx
{
    private DeviceInteraction _rightDeviceInteraction;
    public ArrowShoundPackige ArrowShoundPackige;
    public GameObject CurrentArrow { get; set; }
    public GameObject DelegatePosition;
    private bool _nocked = false;

    public bool Nocked
    {
        get { return _nocked; }
        set
        {
            _nocked = value;
            _animator.SetBool("Pull", _nocked);
        }
    }

    private float Power;
    private float _nockStartPos = 0.2f;
    private float _shoundPos = 0.25f;
    private float _nockMaxPos = 0.7f;
    private float _acceleration = 5f;
    private Animator _animator;

    private AnimationClip _bowPullClip;


    protected override void Start()
    {
        _animator = GetCachedComponent<Animator>();
        _bowPullClip = _animator.runtimeAnimatorController.animationClips.FirstOrDefault(x => x.name == "BowPull");
        _rightDeviceInteraction = DeviceRepository.RightDeviceInteraction;
    }

    // 100 : x = 0.48 : y
    // 0.48x = 100y;
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter Arrow");


        if (other.CompareTag("Arrow"))
        {
            CurrentArrow = other.gameObject;

            TriggerEventSet(true);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit Arrow");


        if (other.CompareTag("Arrow") && !Nocked)
        {
            CurrentArrow = null;

            TriggerEventSet(false);
        }
    }

    private void TriggerEventSet(bool addOrRemove)
    {
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonDownEvent(StartNockArrow, addOrRemove);
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonPressEvent(NockedArrowUpdate, addOrRemove);
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonUpEvent(EndNockArrow, addOrRemove);
    }

    private void StartNockArrow()
    {
        Debug.Log("StartNockArrow");

        ArrowShoundPackige.NockShoundPlay();
        Nocked = true;
    }

    private void EndNockArrow()
    {
        Debug.Log("EndNockArrow");

        if (CurrentArrow == null)
            return;

        ArrowScript arrowScript = CurrentArrow.GetComponent<ArrowScript>();
        
        arrowScript.Shoot(Power);

        ArrowShoundPackige.ShotShoundPlay();

        TriggerEventSet(false);

//        Destroy(CurrentArrow.gameObject, 10);

        CurrentArrow = null;
        Nocked = false;
    }
    
    private void NockedArrowUpdate()
    {
        if (!Nocked)
            return;
        if (CurrentArrow == null)
            return;

        CurrentArrow.transform.rotation = DelegatePosition.transform.rotation;

        DelegatePosition.transform.position = CurrentArrow.transform.position;

        float arrowCurrentPos = DelegatePosition.transform.localPosition.z;

        if (arrowCurrentPos.Equals(_shoundPos))
        {
            Debug.Log("PullShoundPlay");

            ArrowShoundPackige.PullShoundPlay();
        }

        if (arrowCurrentPos > _nockMaxPos)
        {
            arrowCurrentPos = _nockMaxPos;
            _rightDeviceInteraction.StrongVibrationTime(0.3f);
        }
        else if (arrowCurrentPos < _nockStartPos)
        {
            arrowCurrentPos = _nockStartPos;
        }

        ArrowSyncBow(arrowCurrentPos - _nockStartPos);

        DelegatePosition.transform.localPosition = new Vector3(0, 0, arrowCurrentPos);

        CurrentArrow.transform.position = DelegatePosition.transform.position;
    }

    private void ArrowSyncBow(float currnetPos)
    {
        _animator.speed = (currnetPos - _bowPullClip.length) * _acceleration;
        Power = currnetPos * 1000;
    }
}