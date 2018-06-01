using System;
using LandRushLibrary.Unit;

namespace LandRushLibrary.ConcreteUnit
{
    public abstract class Unit<T> where T : UnitInfo
    {
        public T Status { get; set; }

        public abstract void GetDamaged(int damage);

        #region Events
        #region UnitDead
        public event EventHandler<UnitDeadEventArgs> UnitDead;

        protected virtual void OnUnitDead(UnitDeadEventArgs e)
        {
            if (UnitDead != null)
                UnitDead(this, e);
        }

        private UnitDeadEventArgs OnUnitDead()
        {
            UnitDeadEventArgs args = new UnitDeadEventArgs(Status);
            OnUnitDead(args);

            return args;
        }

        private UnitDeadEventArgs OnUnitDeadForOut()
        {
            UnitDeadEventArgs args = new UnitDeadEventArgs(Status);
            OnUnitDead(args);

            return args;
        }

        public class UnitDeadEventArgs : EventArgs
        {
            public T Info { get; set; }

            public UnitDeadEventArgs(T info)
            {
                Info = info;
            }

        }
        #endregion


        #region BeAttacked
        public event EventHandler<BeAttackedEventArgs> BeAttacked;

        protected virtual void OnBeAttacked(BeAttackedEventArgs e)
        {
            if (BeAttacked != null)
                BeAttacked(this, e);
        }

        private BeAttackedEventArgs OnBeAttacked()
        {
            BeAttackedEventArgs args = new BeAttackedEventArgs(Status);
            OnBeAttacked(args);

            return args;
        }

        private BeAttackedEventArgs OnBeAttackedForOut()
        {
            BeAttackedEventArgs args = new BeAttackedEventArgs(Status);
            OnBeAttacked(args);

            return args;
        }

        public class BeAttackedEventArgs : EventArgs
        {

            public T Info { get; set; }

            public BeAttackedEventArgs(T info)
            {
                Info = info;
            }
        }
        #endregion

        #endregion
    }
}
