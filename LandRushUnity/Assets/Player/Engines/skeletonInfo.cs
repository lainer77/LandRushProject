using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class skeletonInfo : IAlive
    {
        public skeletonInfo(float hp, float attack, float defense, float maxHp)
        {
            HP = hp;
            Attack = attack;
            Defense = defense;
            MaxHP = maxHp;
        }
        public float HP { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float MaxHP { get; set; }
    }

