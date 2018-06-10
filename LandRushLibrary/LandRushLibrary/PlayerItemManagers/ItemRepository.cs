using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using LandRushLibrary.Factory;

namespace LandRushLibrary.PlayerItemManagers
{
    public class ItemRepository<T> where T : class, new()
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

        protected ItemRepository()
        {
            _maxItemSlotCount = 12;
            _maxAmount = 10;
            Items = new List<InvenItem>();
        }

        public List<InvenItem> Items { get; protected set; }
        protected int _maxItemSlotCount;
        protected int _maxAmount;

        public void ClearInventory()
        {
            Items = new List<InvenItem>();
        }

        public GameItem GetSlotItemInfo(int slotNum)
        {
            return Items[slotNum].Item;
        }

        public bool AddInvenItem(ItemID itemId, int amount = 1)
        {
            foreach (var invenItem in Items)
            {
                if (invenItem.Item.ItemId == itemId)
                {
                    if (invenItem.Amount + amount <= _maxAmount)
                    {
                        invenItem.Amount += amount;
                        OnInventoryItemChanged(new InventoryItemChangedEventArgs(Items));
                        return true;
                    }

                }
            }

            if (Items.Count < _maxItemSlotCount)
            {
                Items.Add(new InvenItem(ItemFactory.Instance.Create(itemId), 1));
                OnInventoryItemChanged(new InventoryItemChangedEventArgs(Items));
                return true;
            }
            else
            {
                OnInventoryIsFull(new InventoryIsFullEventArgs());
                return false;
            }
        }

        public void ExchangeSlotItem(int source, int target)
        {
            InvenItem temp = Items[target];
            Items[target] = Items[source];
            Items[source] = temp;

            OnInventoryItemChanged(new InventoryItemChangedEventArgs(Items));
        }

        public void RemoveItem(ItemID itemId, int amount)
        {
            var invenItem = (from x in Items
                             where x.Item.ItemId == itemId
                             select x).ToList();

            if (invenItem == null)
                return;

            foreach (var item in invenItem)
            {
                int temp = item.Amount;
                item.Amount -= amount;

                if (item.Amount > 0)
                    break;

                Items.Remove(item);
                amount -= temp;

            }

        }

        public void SetMaxSlotCount(int maxSlotCout)
        {
            _maxItemSlotCount = maxSlotCout;
        }

        public void SetMaxAmount(int maxAmount)
        {
            _maxAmount = maxAmount;
        }


        #region Events

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

        #endregion
        #endregion
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

    public class InventoryIsFullEventArgs : EventArgs
    {

        public InventoryIsFullEventArgs()
        {
        }

    }
}
