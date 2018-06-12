using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using LandRushLibrary.Factory;
using LandRushLibrary.Interfaces;

namespace LandRushLibrary.PlayerItemManagers
{
    public abstract class ItemRepository
    {
        public List<GameItem> Items { get; protected set; }
        protected int _maxItemSlotCount;
        protected int _maxAmount;

        public void ClearInventory()
        {
            Items = new List<GameItem>();
        }

        public GameItem GetSlotItemInfo(int slotNum)
        {
            return Items[slotNum];
        }

        public bool AddGameItem(GameItem gameItem)
        {

            foreach (var item in Items)
            {
                if (item.ItemId == gameItem.ItemId)
                {
                    if (item is ICountable countable)
                    {
                        countable.Amount += ((ICountable) gameItem).Amount;

                        if (countable.MaxAmount < countable.Amount)
                        {
                            int remainAmount = countable.Amount - countable.MaxAmount;
                            countable.Amount = countable.MaxAmount;
                            ((ICountable) gameItem).Amount = remainAmount;
                        }
                        else
                        {
                            OnInventoryItemChanged(Items);
                            return true;
                        }
                    }
                    
                    break;
                }
            }
            


            if (Items.Count < _maxItemSlotCount)
            {
                Items.Add(gameItem);
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
            GameItem temp = Items[target];
            Items[target] = Items[source];
            Items[source] = temp;

            OnInventoryItemChanged(new InventoryItemChangedEventArgs(Items));
        }

        public void RemoveItem(ItemID itemId, int amount)
        {
            var GameItem = (from x in Items
                             where x.ItemId == itemId
                             select x).ToList();

            if (GameItem == null)
                return;


            foreach (var item in GameItem)
            {
                if (item is ICountable countable)
                {
                    int temp = countable.Amount;
                    countable.Amount -= amount;

                    if (countable.Amount > 0)
                        break;

                    Items.Remove(item);
                    amount -= temp;
                }
                else
                {
                    if (amount == 0)
                        break;

                    Items.Remove(item);
                    amount--;
                }

            }

        }



        public int GetAmountForId(ItemID itemId)
        {
            var items = (from x in Items
                where x.ItemId == itemId
                select x).ToList();

            int amount = 0;

            foreach (var gameItem in items)
            {
                if (gameItem is ICountable countable)
                {
                    amount += countable.Amount;
                }
                else
                {
                    amount++;
                }
            }

            return amount;

        }

        #region Events

        #region InventoryItemChanged event things for C# 3.0
        public event EventHandler<InventoryItemChangedEventArgs> InventoryItemChanged;

        protected virtual void OnInventoryItemChanged(InventoryItemChangedEventArgs e)
        {
            if (InventoryItemChanged != null)
                InventoryItemChanged(this, e);
        }

        private InventoryItemChangedEventArgs OnInventoryItemChanged(List<GameItem> GameItems)
        {
            InventoryItemChangedEventArgs args = new InventoryItemChangedEventArgs(GameItems);
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

        public class InventoryItemChangedEventArgs : EventArgs
        {
            public List<GameItem> GameItems { get; set; }

            public InventoryItemChangedEventArgs()
            {
            }

            public InventoryItemChangedEventArgs(List<GameItem> gameItems)
            {
                GameItems = gameItems;
            }
        }

        public class InventoryIsFullEventArgs : EventArgs
        {

            public InventoryIsFullEventArgs()
            {
            }

        }
    }
}
