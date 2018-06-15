using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class PlayerInfo : IAlive
    {
        public PlayerInfo(float hp, float attack, float defense, float maxHp)
        {
            HP = hp;
            Attack = attack;
            Defense = defense;
            MaxHP = maxHp;
            WeaponDefense = 0;
            WeaponAttack = 0;
        }
        public float HP { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float MaxHP { get; set; }
        public float WeaponAttack { get; set; }
        public float WeaponDefense { get; set; }
    }

