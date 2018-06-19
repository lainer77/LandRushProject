using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using UnityEngine;
using UnityScriptHelper;

public class DroppedItem : MonoBehaviourEx
{
    #region outlets
    
    #endregion

    #region fields

    public GameItem ItemInfo { get; set; }

    #endregion

    #region messages
	protected override void Start () 
	{
		
	}
	
	protected override void Update ()
	{
		
	}
    #endregion	

    #region methods

    public void lootingItem()
    {
       if ( PlayerInventory.Instance.AddGameItem(ItemInfo) )
           Destroy(gameObject);

    }

    #endregion
}
