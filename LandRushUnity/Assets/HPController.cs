using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Units;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class HPController : MonoBehaviourEx
{
    private Player _player;

    private float _hpGage;
	// Use this for initialization
	void Start ()
	{
	    _hpGage = GetComponentInChildren<Image>().fillAmount;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _hpGage = _player.CurrentHp;
	}
}
