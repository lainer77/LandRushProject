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
        }

        public List<EquipmentItem> Equipments { get; private set; }
        private EquipmentPair _firstPair;
        private EquipmentPair _secondPair;
        private int _currentPiar;

        public void SetEquipmentToSlot(int slotNum, EquipmentItem equipment)
        {
            Equipments[slotNum] = equipment;

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
            if( _currentPiar == 1)
            {
                _currentPiar = 2;
                Player.Instance.ChangeEquipment( _secondPair.LeftEquipment, _secondPair.RightEquipment );
            }
            else
            {
                _currentPiar = 1;
                Player.Instance.ChangeEquipment(_firstPair.LeftEquipment, _firstPair.RightEquipment);

            }

            OnCurrentPairChanged(new CurrentPairChangedEventArgs());
        }

        private void SetEquipmentPair()
        {
            _firstPair.LeftEquipment = Equipments[0];
            _firstPair.RightEquipment = Equipments[2];

            _secondPair.LeftEquipment = Equipments[1];
            _secondPair.RightEquipment = Equipments[3];
        }

        private class EquipmentPair
        {
            public EquipmentItem LeftEquipment { get; set; } 
            public EquipmentItem RightEquipment { get; set; } 
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
