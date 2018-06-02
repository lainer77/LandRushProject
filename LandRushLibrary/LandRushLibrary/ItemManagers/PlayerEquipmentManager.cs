using LandRushLibrary.Items;
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

            OnSlotItemChanged(new SlotItemChangedEventArgs(Equipments));
        }

        public void ExchangeEquipmentInSlot(int source, int target)
        {
            EquipmentItem temp = Equipments[target];
            Equipments[target] = Equipments[source];
            Equipments[source] = temp;

            OnSlotItemChanged(new SlotItemChangedEventArgs(Equipments));

        }

        public void ChangePair()
        {
            if( _currentPiar == 1)
            {
                _currentPiar = 2;
            }
            else
            {
                _currentPiar = 1;
            }
        }

        private class EquipmentPair
        {
            EquipmentItem LeftEquipment { get; set; } 
            EquipmentItem RightEquipment { get; set; } 
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
    }


}
