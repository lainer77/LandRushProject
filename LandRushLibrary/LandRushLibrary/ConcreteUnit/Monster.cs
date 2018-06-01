using System;
using LandRushLibrary.Combat;

namespace LandRushLibrary.ConcreteUnit
{
    public class Monster : Unit, IAttackable
    {
        public int SlainExp { get; set; }
        public int MonsterType { get; set; }

        public override void GetDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public int GetAttackPower()
        {
            throw new NotImplementedException();
        }

        public void Attack(Unit attakedUnit)
        {
            throw new NotImplementedException();
        }


        public event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;

        public event EventHandler<DoAttackEventArgs> DoAttack;

        protected virtual void OnDoAttack(DoAttackEventArgs e)
        {
            if (DoAttack != null)
                DoAttack(this, e);
        }

        private DoAttackEventArgs OnDoAttack(int attackPower)
        {
            DoAttackEventArgs args = new DoAttackEventArgs(attackPower);
            OnDoAttack(args);

            return args;
        }



    }

}
