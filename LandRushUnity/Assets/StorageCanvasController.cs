using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class StorageCanvasController : MonoBehaviourEx
{
    #region outlets
    
    #endregion

    #region fields

    private GameObject _inventoryCanvas;


    #endregion

    #region messages
	protected override void Awake () 
	{
	    _inventoryCanvas = GameObject.Find("InventoryCanvas");	
	}

    protected override void OnEnable()
    {
        if( _inventoryCanvas == null )
            _inventoryCanvas = GameObject.Find("InventoryCanvas");

        if( _inventoryCanvas.activeSelf )
            _inventoryCanvas.SetActive(false);
    }



    #endregion

    #region methods

    #endregion
}
