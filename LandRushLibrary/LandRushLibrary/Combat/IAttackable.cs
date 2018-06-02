using System;
using LandRushLibrary.Units;

namespace LandRushLibrary.Combat
{
    public interface IAttackable
    {
        void Attack(Unit attakedUnit, bool guard = false, int weaponDamage = 0);
        event EventHandler<CalculatedRandomDamageEventArgs> CalculatedRandomDamage;
    }
}
