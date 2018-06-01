using System;
using LandRushLibrary.Units;

namespace LandRushLibrary.Combat
{
    public interface IAttackable
    {
        void Attack(Unit attakedUnit, bool guard = false);
        event EventHandler<CalculatedRandomDamageEventArgs> CalculatedRandomDamage;
    }
}
