using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class CameraLook : MonoBehaviourEx
{
    private Camera _main;
	// Use this for initialization
    protected override void Start ()
    {
        _main = Camera.main;
    }
	
	// Update is called once per frame
    protected override void Update () {
		transform.LookAt(_main.transform.position);
	}
}
