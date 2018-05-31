using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandRushLibrary.Combat
{
    public class AttackPowerCalulatedEventArgs : EventArgs
    {

        public int AttackPower { get; set; }

        public AttackPowerCalulatedEventArgs()
        {
        }

        public AttackPowerCalulatedEventArgs(int attackPower)
        {
            AttackPower = attackPower;
        }

    }
}
