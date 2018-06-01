
using System;

namespace LandRushLibrary.Combat
{
    public class CalculatedRandomDamageEventArgs : EventArgs
    {
        public int AttackPower { get; set; }

        public CalculatedRandomDamageEventArgs()
        {
        }

        public CalculatedRandomDamageEventArgs(int attackPower)
        {
            AttackPower = attackPower;
        }
    }
}
