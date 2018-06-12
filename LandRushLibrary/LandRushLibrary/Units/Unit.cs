using System;
using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Units
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class Unit
    {
        public string Name { get; set; }
        public int AttackPower { get; set; }
        public int Armor { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public float Speed { get; set; }

        [JsonIgnore] public bool Alive => CurrentHp > 0;

        public Predicate<Unit> InspectCorrectTarget;

        public abstract void GotDamage(int damage);

        #region Dead event things for C# 3.0
        public event EventHandler<DeadEventArgs> Dead;

        protected virtual void OnDead(DeadEventArgs e)
        {
            if (Dead != null)
                Dead(this, e);
        }

        private DeadEventArgs OnDead(Unit unit)
        {
            DeadEventArgs args = new DeadEventArgs(unit);
            OnDead(args);

            return args;
        }

        private DeadEventArgs OnDeadForOut()
        {
            DeadEventArgs args = new DeadEventArgs();
            OnDead(args);

            return args;
        }


        public class DeadEventArgs : EventArgs
        {
            public Unit Unit { get; set; }

            public DeadEventArgs()
            {
            }

            public DeadEventArgs(Unit unit)
            {
                Unit = unit;
            }
        }
        #endregion

        #region Attacked event things for C# 3.0
        public event EventHandler<AttackedEventArgs> Attacked;

        protected virtual void OnAttacked(AttackedEventArgs e)
        {
            if (Attacked != null)
                Attacked(this, e);
        }

        private AttackedEventArgs OnAttacked(Unit attackedUnit)
        {
            AttackedEventArgs args = new AttackedEventArgs(attackedUnit);
            OnAttacked(args);

            return args;
        }

        private AttackedEventArgs OnAttackedForOut()
        {
            AttackedEventArgs args = new AttackedEventArgs();
            OnAttacked(args);

            return args;
        }

        public class AttackedEventArgs : EventArgs
        {
            public Unit AttackedUnit { get; set; }

            public AttackedEventArgs()
            {
            }

            public AttackedEventArgs(Unit attackedUnit)
            {
                AttackedUnit = attackedUnit;
            }
        }
        #endregion
    }


}
