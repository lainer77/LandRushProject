using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

public class Equipmanager : MonoBehaviourEx
{
    #region outlets

    public ItemType ItemType;
    public ItemID ItemId;
    public GameObject Prefab;
    #endregion

    #region fields
    
    #endregion

    #region messages
	protected override void Start ()
	{
	    Instantiate(Prefab);
	}
	
	protected override void Update ()
	{
		
	}
    #endregion	

    #region methods
    
    #endregion
}
