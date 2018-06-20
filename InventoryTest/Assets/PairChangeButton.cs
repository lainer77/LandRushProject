using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Units;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class PairChangeButton : MonoBehaviourEx
{
    #region outlets

    #endregion

    #region fields
    private DeviceInteraction _rightController;

    public Sprite EnterImage;
    public Sprite ExitImage;
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
	        Image image = GetComponent<Image>();
	        image.sprite = EnterImage;

	        if (_rightController.Controller.GetHairTriggerDown())
	        {
	            Player.Instance.Equipment.ChangeNextPair();
	        }
	    }
	    else
	    {
	        Image image = GetComponent<Image>();
	        image.sprite = ExitImage;
        }

    }
    #endregion	

    #region methods
    
    #endregion
}
