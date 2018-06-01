using System;
using LandRushLibrary.ConcreteUnit;
using LandRushLibrary.Repository;

namespace LandRushLibrary.Combat
{
    public interface IAttackable
    {
        int GetAttackPower();
        void Attack(Unit attakedUnit, int attackType = AttackType.NORMARL);
        event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;
    }
}
