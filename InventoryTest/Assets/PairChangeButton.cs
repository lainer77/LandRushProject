using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Units;
using UnityEngine;
using UnityScriptHelper;

public class PairChangeButton : MonoBehaviourEx
{
    #region outlets

    #endregion

    #region fields
    private DeviceInteraction _rightController;
    #endregion

    #region messages
    protected override void Start () 
	{
	    _rightController = DeviceRepository.RightDeviceInteraction;
    }
	
	protected override void Update ()
	{
	    RaserCraft laser = _rightController.GetComponentInChildren<RaserCraft>();

	    if (laser.Hit.transform == null)
	        return;

	    if (laser.Hit.transform.gameObject == gameObject)
	    {

	        if (_rightController.Controller.GetHairTriggerDown())
	        {
	            Player.Instance.Equipment.ChangeNextPair();
	        }
	    }

    }
    #endregion	

    #region methods
    
    #endregion
}
