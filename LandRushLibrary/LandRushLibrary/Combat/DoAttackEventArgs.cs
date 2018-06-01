
using System;

namespace LandRushLibrary.Combat
{
    public class DoAttackEventArgs : EventArgs
    {
        public int AttackPower { get; set; }

        public DoAttackEventArgs()
        {
        }

        public DoAttackEventArgs(int attackPower)
        {
            AttackPower = attackPower;
        }
    }
}
