using LandRushLibrary.Items;
using LandRushLibrary.Units;
using System;
using System.Collections.Generic;

namespace LandRushLibrary.ItemManagers
{
    public class PlayerEquipmentManager
    {
        private static PlayerEquipmentManager _instance;
        public static PlayerEquipmentManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerEquipmentManager();

                return _instance;
            }
        }

        private PlayerEquipmentManager()
        {
            Equipments = new List<EquipmentItem>();
            _firstPair = new EquipmentPair();
            _secondPair = new EquipmentPair();
            _currentPair = _firstPair;

            Equipments = new List<EquipmentItem>();
            for(int i = 0; i < 4; i++)
            {
                Equipments.Add(new DummyEquipment());
            }
        }

        public List<EquipmentItem> Equipments { get; private set; }
        private EquipmentPair _firstPair;
        private EquipmentPair _secondPair;
        private EquipmentPair _currentPair;

        

        public void SetEquipmentToSlot(int slotNum, EquipmentItem equipment)
        {
            Equipments[slotNum - 1] = equipment;

            SetEquipmentPair();

            OnSlotItemChanged(new SlotItemChangedEventArgs(Equipments));
        }

        public void ExchangeEquipmentInSlot(int source, int target)
        {
            EquipmentItem temp = Equipments[target];
            Equipments[target] = Equipments[source];
            Equipments[source] = temp;

            SetEquipmentPair();

            OnSlotItemChanged(new SlotItemChangedEventArgs(Equipments));

        }

        public void ChangeEquipmentPair()
        {
            if( _currentPair == _firstPair )
                _currentPair = _secondPair;
            else
                _currentPair = _firstPair;

            EquipCurrentPair();

            OnCurrentPairChanged(new CurrentPairChangedEventArgs());
        }

        public void EquipCurrentPair()
        {
            Player.Instance.ChangeEquipment(_currentPair.LeftEquipment, _currentPair.RightEquipment);

        }

        private void SetEquipmentPair()
        {
            _firstPair.RightEquipment = Equipments[0];
            _firstPair.LeftEquipment = Equipments[2];

            _secondPair.RightEquipment = Equipments[1];
            _secondPair.LeftEquipment = Equipments[3];
        }

        private class EquipmentPair
        {
            public EquipmentItem LeftEquipment { get; set; } 
            public EquipmentItem RightEquipment { get; set; } 
        }

        private class DummyEquipment : EquipmentItem
        {
            public override GameItem Clone()
            {
                throw new NotImplementedException();
            }
        }

        #region SlotItemChanged event things for C# 3.0
        public event EventHandler<SlotItemChangedEventArgs> SlotItemChanged;

        protected virtual void OnSlotItemChanged(SlotItemChangedEventArgs e)
        {
            if (SlotItemChanged != null)
                SlotItemChanged(this, e);
        }

        private SlotItemChangedEventArgs OnSlotItemChanged(List<EquipmentItem> slotItems)
        {
            SlotItemChangedEventArgs args = new SlotItemChangedEventArgs(slotItems);
            OnSlotItemChanged(args);

            return args;
        }

        private SlotItemChangedEventArgs OnSlotItemChangedForOut()
        {
            SlotItemChangedEventArgs args = new SlotItemChangedEventArgs();
            OnSlotItemChanged(args);

            return args;
        }

        public class SlotItemChangedEventArgs : EventArgs
        {
            public List<EquipmentItem> SlotItems { get; set; }

            public SlotItemChangedEventArgs()
            {
            }

            public SlotItemChangedEventArgs(List<EquipmentItem> slotItems)
            {
                SlotItems = slotItems;
            }
        }
        #endregion

        #region CurrentPairChanged event things for C# 3.0
        public event EventHandler<CurrentPairChangedEventArgs> CurrentPairChanged;

        protected virtual void OnCurrentPairChanged(CurrentPairChangedEventArgs e)
        {
            if (CurrentPairChanged != null)
                CurrentPairChanged(this, e);
        }

        private CurrentPairChangedEventArgs OnCurrentPairChanged()
        {
            CurrentPairChangedEventArgs args = new CurrentPairChangedEventArgs();
            OnCurrentPairChanged(args);

            return args;
        }

        private CurrentPairChangedEventArgs OnCurrentPairChangedForOut()
        {
            CurrentPairChangedEventArgs args = new CurrentPairChangedEventArgs();
            OnCurrentPairChanged(args);

            return args;
        }

        public class CurrentPairChangedEventArgs : EventArgs
        {


            public CurrentPairChangedEventArgs()
            {
            }

        }
        #endregion
    }


}
