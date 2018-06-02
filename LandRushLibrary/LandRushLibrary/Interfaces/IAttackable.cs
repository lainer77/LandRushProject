using System;
using LandRushLibrary.Units;
using LandRushLibrary.Combat;

namespace LandRushLibrary
{
    public interface IAttackable
    {
        void Attack(Unit attakedUnit, bool guard = false, int weaponDamage = 0);
        event EventHandler<CalculatedRandomDamageEventArgs> CalculatedRandomDamage;
    }
}
