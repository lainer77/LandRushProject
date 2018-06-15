using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Sword : MonoBehaviour, IWeapon
{
    enum sword
    {
        Attack = 10,
        Defense = 0
    }

    public Sword()
    {
        Attack = (float) sword.Attack;
        Defense = (float) sword.Defense;
    }

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        playerAttack = player.GetComponent<Player_Controller>().Player.Attack;
    }

    public float Attack { get; set; }
    public float Defense { get; set; }
    public float playerAttack { get; set; }

    void OnTriggerEnter(Collider other)
    {
//        if(other.tag == "Enemy")
//        other.GetComponent<Skeleton_Controller>().skeleton.HP -=
//            Attack + playerAttack - other.GetComponent<Skeleton_Controller>().skeleton.Defense;
    }
}