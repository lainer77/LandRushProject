using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandRushLibrary.ConcreteUnit;

namespace LandRushLibrary.Combat
{
    public interface IAttackable
    {
        int GetAttackPower();
        event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;
    }
}
