using System;
using LandRushLibrary.Units;

namespace LandRushLibrary
{
    public interface IAttackable
    {
        void Attack(Unit attakedUnit, int weaponDamage = 0);
        event EventHandler<CalculatedRandomDamageEventArgs> CalculatedRandomDamage;
    }

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
