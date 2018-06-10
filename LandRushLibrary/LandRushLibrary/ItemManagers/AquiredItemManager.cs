using LandRushLibrary.Items;
using System;
using System.Collections.Generic;

namespace LandRushLibrary.ItemManagers
{
    public class AquiredItemManager<T> where T : class, new()
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }

        protected AquiredItemManager()
        {
            _maxItemSlot = 12;
        }

        public List<InvenItem> InvenItems { get; private set; }
        protected int _maxItemSlot;
        protected int _maxAmmount;

        public void AddInvenItem(GameItem item)
        {
            foreach (var invenItem in InvenItems)
            {
                if( invenItem.Item.ItemId == item.ItemId )
                {
                    if (invenItem.Ammount < _maxAmmount)
                    {
                        invenItem.Ammount++;
                        OnInventoryItemChanged(new InventoryItemChangedEventArgs(InvenItems));
                        return;
                    }
                    
                }
            }

            if (InvenItems.Count < _maxItemSlot)
            {
                InvenItems.Add(new InvenItem(item, 1));
                OnInventoryItemChanged(new InventoryItemChangedEventArgs(InvenItems));
            }
            else
                OnInventoryIsFull(new InventoryIsFullEventArgs());

        }

        public void ExchangeSlotItem(int source, int target)
        {
            InvenItem temp = InvenItems[target];
            InvenItems[target] = InvenItems[source];
            InvenItems[source] = temp;

            OnInventoryItemChanged(new InventoryItemChangedEventArgs(InvenItems));
        }

        #region InventoryItemChanged event things for C# 3.0
        public event EventHandler<InventoryItemChangedEventArgs> InventoryItemChanged;

        protected virtual void OnInventoryItemChanged(InventoryItemChangedEventArgs e)
        {
            if (InventoryItemChanged != null)
                InventoryItemChanged(this, e);
        }

        private InventoryItemChangedEventArgs OnInventoryItemChanged(List<InvenItem> invenItems)
        {
            InventoryItemChangedEventArgs args = new InventoryItemChangedEventArgs(invenItems);
            OnInventoryItemChanged(args);

            return args;
        }

        private InventoryItemChangedEventArgs OnInventoryItemChangedForOut()
        {
            InventoryItemChangedEventArgs args = new InventoryItemChangedEventArgs();
            OnInventoryItemChanged(args);

            return args;
        }

        public class InventoryItemChangedEventArgs : EventArgs
        {
            public List<InvenItem> InvenItems { get; set; }

            public InventoryItemChangedEventArgs()
            {
            }

            public InventoryItemChangedEventArgs(List<InvenItem> invenItems)
            {
                InvenItems = invenItems;
            }
        }
        #endregion

        #region InventoryIsFull event things for C# 3.0
        public event EventHandler<InventoryIsFullEventArgs> InventoryIsFull;

        protected virtual void OnInventoryIsFull(InventoryIsFullEventArgs e)
        {
            if (InventoryIsFull != null)
                InventoryIsFull(this, e);
        }

        private InventoryIsFullEventArgs OnInventoryIsFull()
        {
            InventoryIsFullEventArgs args = new InventoryIsFullEventArgs();
            OnInventoryIsFull(args);

            return args;
        }

        private InventoryIsFullEventArgs OnInventoryIsFullForOut()
        {
            InventoryIsFullEventArgs args = new InventoryIsFullEventArgs();
            OnInventoryIsFull(args);

            return args;
        }

        public class InventoryIsFullEventArgs : EventArgs
        {

            public InventoryIsFullEventArgs()
            {
            }

        }
        #endregion
    }
}
