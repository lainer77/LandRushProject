using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class StatusUIConstroller : MonoBehaviourEx
{
    #region outlets
    
    #endregion

    #region fields

    private Transform _cameraTranstorm;

    #endregion

    #region messages
	protected override void Start ()
	{
	    _cameraTranstorm = GameObject.Find("[CameraRig]").transform;
	}
	
	protected override void LateUpdate ()
	{
		transform.LookAt(_cameraTranstorm.position);
	}
    #endregion	

    #region methods
    
    #endregion
}
