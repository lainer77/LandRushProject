using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class PopUIController : MonoBehaviourEx
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
        transform.Rotate(new Vector3(0, 180, 0));
	}
    #endregion	

    #region methods
    
    #endregion
}
