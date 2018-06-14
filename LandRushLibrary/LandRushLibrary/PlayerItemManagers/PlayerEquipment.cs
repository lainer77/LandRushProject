using System;
using LandRushLibrary.Items;

namespace LandRushLibrary.PlayerItemManagers
{
    public class PlayerEquipment
    {
        #region Constructor

        public PlayerEquipment(int maxPairCount)
        {
            _maxPairCount = maxPairCount;
            _equipmentPairs = new EquipmentPair[_maxPairCount];
            CurrentPair = 1;

            for (int i = 0; i < _equipmentPairs.Length; i++)
            {
                _equipmentPairs[i] = new EquipmentPair();
            }
        }

        public void EquipItem(EquipmentSlot right, object itemFctory)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Fields
        private readonly int _maxPairCount;
        public int CurrentPair { get; private set; }
        private EquipmentPair[] _equipmentPairs;

        public EquipmentItem LeftEquipment
        {
            get
            {
                return _equipmentPairs[CurrentPair - 1].LeftEquipment;
            }

            private set
            {
                _equipmentPairs[CurrentPair - 1].LeftEquipment = value;
            }
        }

        public EquipmentItem RightEquipment
        {
            get
            {
                return _equipmentPairs[CurrentPair - 1].RightEquipment;
            }

            private set
            {
                _equipmentPairs[CurrentPair - 1].RightEquipment = value;
            }
        }
        #endregion

        #region  Methods

        public void EquipItem(EquipmentSlot equipmentSlot, EquipmentItem equipment )
        {
            if (equipmentSlot == EquipmentSlot.Left)
                _equipmentPairs[CurrentPair - 1].LeftEquipment = equipment;
            else if (equipmentSlot == EquipmentSlot.Right)
                _equipmentPairs[CurrentPair - 1].RightEquipment = equipment;

            OnEquipmentChanged(equipment, equipmentSlot);
        }

        public void ChangeNextPair()
        {
            if (CurrentPair >= _maxPairCount)
                CurrentPair = 1;
            else
                CurrentPair++;
            OnCurrentPairChanged(this);

        }

        #endregion

        #region Events
        #region CurrentPairChanged
        public event EventHandler<CurrentPairChangedEventArgs> CurrentPairChanged;

        protected virtual void OnCurrentPairChanged(CurrentPairChangedEventArgs e)
        {
            if (CurrentPairChanged != null)
                CurrentPairChanged(this, e);
        }

        private CurrentPairChangedEventArgs OnCurrentPairChanged(PlayerEquipment playerEquipment)
        {
            CurrentPairChangedEventArgs args = new CurrentPairChangedEventArgs(playerEquipment);
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
            public PlayerEquipment PlayerEquipment { get; set; }

            public CurrentPairChangedEventArgs()
            {
            }

            public CurrentPairChangedEventArgs(PlayerEquipment playerEquipment)
            {
                PlayerEquipment = playerEquipment;
            }
        }
        #endregion

        #region EquipmentChanged
        public event EventHandler<EquipmentChangedEventArgs> EquipmentChanged;

        protected virtual void OnEquipmentChanged(EquipmentChangedEventArgs e)
        {
            if (EquipmentChanged != null)
                EquipmentChanged(this, e);
        }

        private EquipmentChangedEventArgs OnEquipmentChanged(EquipmentItem equipment, EquipmentSlot equipmentSlot)
        {
            EquipmentChangedEventArgs args = new EquipmentChangedEventArgs(equipment, equipmentSlot);
            OnEquipmentChanged(args);

            return args;
        }

        private EquipmentChangedEventArgs OnEquipmentChangedForOut()
        {
            EquipmentChangedEventArgs args = new EquipmentChangedEventArgs();
            OnEquipmentChanged(args);

            return args;
        }

        public class EquipmentChangedEventArgs : EventArgs
        {
            public EquipmentItem EquipmentItem { get; set; }
            public EquipmentSlot EquipmentSlot { get; set; }

            public EquipmentChangedEventArgs()
            {
            }

            public EquipmentChangedEventArgs(EquipmentItem equipmentItem, EquipmentSlot equipmentSlot)
            {
                EquipmentItem = equipmentItem;
                EquipmentSlot = EquipmentSlot;
            }
        }
        #endregion
        #endregion

        public class EquipmentPair
        {
            public EquipmentItem LeftEquipment { get; set; }
            public EquipmentItem RightEquipment { get; set; }

        }
    }

    public enum EquipmentSlot
    {
        Left,
        Right
    }

}
