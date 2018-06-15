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
            EquipmentPairs = new EquipmentPair[_maxPairCount];
            _currentIndex = 1;

            for (int i = 0; i < EquipmentPairs.Length; i++)
            {
                EquipmentPairs[i] = new EquipmentPair();
            }
        }

        #endregion

        #region Fields
        private readonly int _maxPairCount;
        private int _currentIndex;
        public EquipmentPair CurrentPair
        {
            get
            {
                return EquipmentPairs[_currentIndex];
            }
        }

        public EquipmentPair[] EquipmentPairs { get; set; }

        public EquipmentItem LeftEquipment
        {
            get
            {
                return EquipmentPairs[_currentIndex - 1].LeftEquipment;
            }

            private set
            {
                EquipmentPairs[_currentIndex - 1].LeftEquipment = value;
            }
        }

        public EquipmentItem RightEquipment
        {
            get
            {
                return EquipmentPairs[_currentIndex - 1].RightEquipment;
            }

            private set
            {
                EquipmentPairs[_currentIndex - 1].RightEquipment = value;
            }
        }
        #endregion

        #region  Methods

        public void EquipItem(EquipmentSlot equipmentSlot, EquipmentItem equipment )
        {
            EquipmentItem prevEqupment = null;

            if (equipmentSlot == EquipmentSlot.Left)
            {
                prevEqupment = EquipmentPairs[_currentIndex - 1].LeftEquipment;
                EquipmentPairs[_currentIndex - 1].LeftEquipment = equipment;
            }
            else if (equipmentSlot == EquipmentSlot.Right)
            {
                prevEqupment = EquipmentPairs[_currentIndex - 1].RightEquipment;
                EquipmentPairs[_currentIndex - 1].RightEquipment = equipment;
            }

            OnEquipmentChanged(prevEqupment, equipment, equipmentSlot);
        }

        public void ChangeNextPair()
        {
            EquipmentPair prevPair = EquipmentPairs[_currentIndex - 1];

            if (_currentIndex >= _maxPairCount)
                _currentIndex = 1;
            else
                _currentIndex++;

            EquipmentPair newPair = EquipmentPairs[_currentIndex - 1];


            OnCurrentPairChanged(prevPair, newPair);

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

        private CurrentPairChangedEventArgs OnCurrentPairChanged(EquipmentPair prevPair, EquipmentPair newPair)
        {
            CurrentPairChangedEventArgs args = new CurrentPairChangedEventArgs(prevPair, newPair);
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
            public EquipmentPair PrevPair { get; set; }
            public EquipmentPair NewPair { get; set; }

            public CurrentPairChangedEventArgs()
            {
            }

            public CurrentPairChangedEventArgs(EquipmentPair prevPair, EquipmentPair newPair)
            {
                PrevPair = prevPair;
                NewPair = newPair;
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

        private EquipmentChangedEventArgs OnEquipmentChanged(EquipmentItem newEquipment, EquipmentItem prevEquipment, EquipmentSlot equipmentSlot)
        {
            EquipmentChangedEventArgs args = new EquipmentChangedEventArgs(newEquipment, prevEquipment, equipmentSlot);
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
            public EquipmentItem PrevEquipment { get; set; }
            public EquipmentItem NewEquipment { get; set; }
            public EquipmentSlot EquipmentSlot { get; set; }

            public EquipmentChangedEventArgs()
            {
            }

            public EquipmentChangedEventArgs(EquipmentItem newEquipment, EquipmentItem prevEquipment, EquipmentSlot equipmentSlot)
            {
                PrevEquipment = prevEquipment;
                NewEquipment = newEquipment;
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
