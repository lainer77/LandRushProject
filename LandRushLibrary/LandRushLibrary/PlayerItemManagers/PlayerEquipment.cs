using System;
using LandRushLibrary.Items;

namespace LandRushLibrary.PlayerItemManagers
{
    public class PlayerEquipment
    {
        public PlayerEquipment(int maxPairCount)
        {
            _maxPairCount = maxPairCount;
            _equipmentPairs = new EquipmentPair[_maxPairCount];

        }

        private readonly int _maxPairCount;
        public int CurrentPair { get; private set; }
        private EquipmentPair[] _equipmentPairs;

        public EquipmentItem LeftEquipment { get; private set; }
        public EquipmentItem RightEquipment { get; private set; }

        public void EquipItem(EquipmentSlot equipmentSlot, EquipmentItem equipment)
        {
            if (equipmentSlot == EquipmentSlot.Left)
                _equipmentPairs[CurrentPair - 1].LeftEquipment = equipment;
            else if (equipmentSlot == EquipmentSlot.Right)
                _equipmentPairs[CurrentPair - 1].RightEquipment = equipment;

            OnEquipmentChanged(this);
        }

        public void ChangeNextPair()
        {
            if (CurrentPair >= _maxPairCount)
                CurrentPair = 1;
            else
                CurrentPair++;

            RightEquipment = _equipmentPairs[CurrentPair - 1].RightEquipment;
            LeftEquipment = _equipmentPairs[CurrentPair - 1].LeftEquipment;

            OnCurrentPairChanged(this);

        }

        private class EquipmentPair
        {
            public EquipmentItem LeftEquipment { get; set; }
            public EquipmentItem RightEquipment { get; set; }

        }


        #region CurrentPairChanged event things for C# 3.0
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

        #region EquipmentChanged event things for C# 3.0
        public event EventHandler<EquipmentChangedEventArgs> EquipmentChanged;

        protected virtual void OnEquipmentChanged(EquipmentChangedEventArgs e)
        {
            if (EquipmentChanged != null)
                EquipmentChanged(this, e);
        }

        private EquipmentChangedEventArgs OnEquipmentChanged(PlayerEquipment playerEquipment)
        {
            EquipmentChangedEventArgs args = new EquipmentChangedEventArgs(playerEquipment);
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
            public PlayerEquipment PlayerEquipment { get; set; }

            public EquipmentChangedEventArgs()
            {
            }

            public EquipmentChangedEventArgs(PlayerEquipment playerEquipment)
            {
                PlayerEquipment = playerEquipment;
            }
        }
        #endregion

    }

    public enum EquipmentSlot
    {
        Left,
        Right
    }

}
