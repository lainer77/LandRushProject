using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using UnityEngine;
using UnityScriptHelper;

public class ArrowScript : MonoBehaviourEx 
{
	public Arrow Arrow { get; set; }

	private Rigidbody _rigidbody;

	protected override void Awake()
	{
		_rigidbody = GetCachedComponent<Rigidbody>();
	}

	public void Shoot(float power)
	{
		_rigidbody.AddForce(Vector3.forward * power);
	}
}
