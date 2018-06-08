﻿using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using UnityEngine;
using UnityScriptHelper;

public class ArrowScript : MonoBehaviourEx 
{
    public enum ArrowState
    {
        Shoot,
        Hand,
        Nocked
    }
	public Arrow Arrow { get; set; }
    public Rigidbody Head;
	private Rigidbody _rigidbody;
    private ArrowState _state;

    public ArrowState State
    {
        get { return _state; }
        set
        {
            _state = value;
            if (_state == ArrowState.Hand)
            {
                UnUseGravity(_rigidbody);
                UnUseGravity(Head);
            }
            else if (_state == ArrowState.Nocked)
            {
                
            }
            else
            {
                UseGravity(_rigidbody);
                UseGravity(Head);
            }
        }
    }

    protected override void Awake()
	{
		_rigidbody = GetCachedComponent<Rigidbody>(); 

    }

    protected override void OnTransformParentChanged()
    {
        if (transform.parent != null && transform.parent.CompareTag(Tags.RightController))
        {
            State = ArrowState.Hand;
        }
    }

    public void Shoot(float power)
	{
        transform.SetParent(null);

	    State = ArrowState.Shoot;
        Debug.Log("Shoot is Power = " + power);
	    Head.AddForce(Vector3.back * power);

        Destroy(gameObject, 10);
	}

    private void UseGravity(Rigidbody rigidbody)
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
    }
    private void UnUseGravity(Rigidbody rigidbody)
    {
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }
}
