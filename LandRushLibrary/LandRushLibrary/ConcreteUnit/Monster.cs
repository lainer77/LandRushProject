using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Unit;

namespace LandRushLibrary.ConcreteUnit
{
    public class Monster : Unit<MonsterInfo>, IAttackable
    {
        public Monster(int unitId)
        {
            Status = UnitInfoRepository.Instance.GetMonsterInfo(unitId);
        }

        public override void Damaged(int damage)
        {
            Status.CurrentHp -= damage;

            if (Status.CurrentHp <= 0)
            {
                OnUnitDead(new UnitDeadEventArgs(Status));
            }
        }

        public int GetAttackPower(int attackType)
        {
            AttackPowerCalulatedEventArgs args = new AttackPowerCalulatedEventArgs(Status.AttackPower, attackType);

            OnAttackPowerCalulated(args);

            return args.AttackPower;
        }

        #region Events

        public event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;

        protected virtual void OnAttackPowerCalulated(AttackPowerCalulatedEventArgs e)
        {
            if (AttackPowerCalulated != null)
                AttackPowerCalulated(this, e);

        }

        private AttackPowerCalulatedEventArgs OnAttackPowerCalulated(int attackPower, int attackType)
        {
            AttackPowerCalulatedEventArgs args = new AttackPowerCalulatedEventArgs(attackPower, attackType);
            OnAttackPowerCalulated(args);

            return args;
        }

        private AttackPowerCalulatedEventArgs OnAttackPowerCalulatedForOut()
        {
            AttackPowerCalulatedEventArgs args = new AttackPowerCalulatedEventArgs();
            OnAttackPowerCalulated(args);

            return args;
        }


        #endregion
    }

}
