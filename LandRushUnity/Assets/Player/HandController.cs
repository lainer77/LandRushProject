using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    private SteamVR_TrackedObject _trackedObject;
    private SteamVR_Controller.Device _device;
	// Use this for initialization
	void Awake()
    {
        _trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        _device = SteamVR_Controller.Input((int)_trackedObject.index);
	}

    public void onVive()
    {
        _device.TriggerHapticPulse(1200);
    }
}
