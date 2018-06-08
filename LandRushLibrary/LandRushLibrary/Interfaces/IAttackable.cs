using System;
using LandRushLibrary.Units;
using LandRushLibrary.Combat;

namespace LandRushLibrary
{
    public interface IAttackable
    {
        void Attack(Unit attakedUnit, int weaponDamage = 0, bool guard = false);
        event EventHandler<CalculatedRandomDamageEventArgs> CalculatedRandomDamage;
    }
}
