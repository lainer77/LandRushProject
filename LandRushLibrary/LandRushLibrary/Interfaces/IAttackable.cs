using System;
using LandRushLibrary.Units;

namespace LandRushLibrary
{
    public interface IAttackable
    {
        void Attack(Unit attakedUnit, int weaponDamage = 0);
        event EventHandler<DamageCalculatedEventArgs> DamageCalculated;
    }

    public class DamageCalculatedEventArgs : EventArgs
    {
        public int AttackPower { get; set; }

        public DamageCalculatedEventArgs()
        {
        }

        public DamageCalculatedEventArgs(int attackPower)
        {
            AttackPower = attackPower;
        }
    }
}
