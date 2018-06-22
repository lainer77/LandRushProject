using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class PopUIController : MonoBehaviourEx
{
    #region outlets
    
    #endregion

    #region fields

    #endregion


    #region messages
	protected override void Start ()
	{
    }

    protected override void Update()
    {
                
    }

    protected override void OnEnable()
    {
        Vector3 dir = Camera.main.transform.TransformDirection(Vector3.forward);
        
        transform.position = Camera.main.transform.position + dir * 3.5f;

    }
	
	protected override void LateUpdate ()
	{
		transform.LookAt(Camera.main.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
	}
    #endregion	

    #region methods


    #endregion
}
