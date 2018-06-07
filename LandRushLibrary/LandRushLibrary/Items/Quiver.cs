using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Quiver : EquipmentItem
    {
        public Quiver(int limitChargingNum)
        {
            LimitChargingNum = limitChargingNum;
        }

        private Stack<Arrow> Arrows { get; } = new Stack<Arrow>();
        public int LimitChargingNum { get; } = 30;

        public void ArrowCharging(int chargingArrowNum)
        {
            for (int i = 0; i < chargingArrowNum; i++)
            {
                if (Arrows.Count + chargingArrowNum >= LimitChargingNum)
                {
                    OnQuiverFullCharging(Arrows, LimitChargingNum, chargingArrowNum);
                    break;
                }
                Arrow newArrow = new Arrow();
                OnNewArrowAdd(newArrow, Arrows.Count);
                Arrows.Push(newArrow);
            }
        }

        public Arrow GetArrow()
        {
            if (Arrows.Count == 0)
            {
                OnNoArrowPop(Arrows);
                return null;
            }
            return Arrows.Pop();
        }
        public override GameItem Clone()
        {
            Quiver clone = new Quiver(LimitChargingNum);

            SetBasicCloneItem(clone);
            return clone;
        }

        #region NoArrowPop event things for C# 3.0
        public event EventHandler<NoArrowPopEventArgs> NoArrowPop;

        protected virtual void OnNoArrowPop(NoArrowPopEventArgs e)
        {
            if (NoArrowPop != null)
                NoArrowPop(this, e);
        }

        private NoArrowPopEventArgs OnNoArrowPop(Stack<Arrow> arrows)
        {
            NoArrowPopEventArgs args = new NoArrowPopEventArgs(arrows);
            OnNoArrowPop(args);

            return args;
        }

        private NoArrowPopEventArgs OnNoArrowPopForOut()
        {
            NoArrowPopEventArgs args = new NoArrowPopEventArgs();
            OnNoArrowPop(args);

            return args;
        }

        public class NoArrowPopEventArgs : EventArgs
        {
            public Stack<Arrow> Arrows { get; set; }

            public NoArrowPopEventArgs()
            {
            }

            public NoArrowPopEventArgs(Stack<Arrow> arrows)
            {
                Arrows = arrows;
            }
        }
        #endregion
        #region NewArrowAdd event things for C# 3.0
        public event EventHandler<NewArrowAddEventArgs> NewArrowAdd;

        protected virtual void OnNewArrowAdd(NewArrowAddEventArgs e)
        {
            if (NewArrowAdd != null)
                NewArrowAdd(this, e);
        }

        private NewArrowAddEventArgs OnNewArrowAdd(Arrow newArrow, int currentArrowNum)
        {
            NewArrowAddEventArgs args = new NewArrowAddEventArgs(newArrow, currentArrowNum);
            OnNewArrowAdd(args);

            return args;
        }

        private NewArrowAddEventArgs OnNewArrowAddForOut()
        {
            NewArrowAddEventArgs args = new NewArrowAddEventArgs();
            OnNewArrowAdd(args);

            return args;
        }

        public class NewArrowAddEventArgs : EventArgs
        {
            public Arrow NewArrow { get; set; }
            public int CurrentArrowNum { get; set; }

            public NewArrowAddEventArgs()
            {
            }

            public NewArrowAddEventArgs(Arrow newArrow, int currentArrowNum)
            {
                NewArrow = newArrow;
                CurrentArrowNum = currentArrowNum;
            }
        }
        #endregion
        #region QuiverFullCharging event things for C# 3.0

        public event EventHandler<QuiverFullChargingEventArgs> QuiverFullCharging;

        protected virtual void OnQuiverFullCharging(QuiverFullChargingEventArgs e)
        {
            if (QuiverFullCharging != null)
                QuiverFullCharging(this, e);
        }

        private QuiverFullChargingEventArgs OnQuiverFullCharging(Stack<Arrow> arrows, int limitChargingNum,
            int chargingArrowNum)
        {
            QuiverFullChargingEventArgs args =
                new QuiverFullChargingEventArgs(arrows, limitChargingNum, chargingArrowNum);
            OnQuiverFullCharging(args);

            return args;
        }

        private QuiverFullChargingEventArgs OnQuiverFullChargingForOut()
        {
            QuiverFullChargingEventArgs args = new QuiverFullChargingEventArgs();
            OnQuiverFullCharging(args);

            return args;
        }

        public class QuiverFullChargingEventArgs : EventArgs
        {
            public Stack<Arrow> Arrows { get; set; }
            public int LimitChargingNum { get; set; }
            public int ChargingArrowNum { get; set; }

            public QuiverFullChargingEventArgs()
            {
            }

            public QuiverFullChargingEventArgs(Stack<Arrow> arrows, int limitChargingNum, int chargingArrowNum)
            {
                Arrows = arrows;
                LimitChargingNum = limitChargingNum;
                ChargingArrowNum = chargingArrowNum;
            }
        }

        #endregion
    }
}