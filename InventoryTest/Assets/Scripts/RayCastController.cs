using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class RayCastController : MonoBehaviourEx
{
    #region outlets

    #endregion

    #region fields
    private RaserCraft _laser;
    private DeviceInteraction _rightController;
    #endregion

    #region messages
    protected override void Awake () 
	{
	    _rightController = DeviceRepository.RightDeviceInteraction;
	    _laser = _rightController.GetComponentInChildren<RaserCraft>();
    }
	
	protected override void Update ()
	{
	    if ( _rightController.Controller.GetHairTrigger())
	    {
	        if (_laser.Hit.transform == null)
	            return;

	        if (_laser.Hit.transform.gameObject.CompareTag("Item"))
	        {
	            _laser.Hit.transform.gameObject.GetComponent<DroppedItem>().lootingItem();
	        }
	    }
    }
    #endregion	

    #region methods
    
    #endregion
}
