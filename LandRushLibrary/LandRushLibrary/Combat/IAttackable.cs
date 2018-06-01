using System;
using LandRushLibrary.ConcreteUnit;

namespace LandRushLibrary.Combat
{
    public interface IAttackable
    {
        int GetAttackPower();
        void Attack(Unit attakedUnit);
        
        event EventHandler<DoAttackEventArgs> DoAttack;
    }
}
