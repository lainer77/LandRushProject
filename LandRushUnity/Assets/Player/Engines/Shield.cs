using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Shield : MonoBehaviour, IWeapon

{
    enum shield
    {
        Attack = 0, Defense = 5
    }
    public Shield()
    {
        Attack = (float)shield.Attack;
        Defense = (float)shield.Defense;
    }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float playerAttack { get; set; }

    private HandController _handController;

    void Start()
    {
        _handController = GameObject.Find("Controller (left)").GetComponent<HandController>();

    }
    void OnTriggerEnter()
    {
        _handController.onVive();
    }

}

