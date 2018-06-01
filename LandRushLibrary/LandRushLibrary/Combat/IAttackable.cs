using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LandRushLibrary.ConcreteUnit;

namespace LandRushLibrary.Combat
{
    public interface IAttackable
    {
        int GetAttackPower(int attackType);
        event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;
    }

    public class AttackPowerCalulatedEventArgs : EventArgs
    {
        public int AttackPower { get; set; }
        public int AttackType { get; set; }

        public AttackPowerCalulatedEventArgs()
        {
        }

        public AttackPowerCalulatedEventArgs(int attackPower, int attackType)
        {
            AttackPower = attackPower;
        }

    }
}
