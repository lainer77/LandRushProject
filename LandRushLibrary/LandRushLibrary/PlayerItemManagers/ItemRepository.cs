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
        #region Fields

        public GameItem[,] Items { get; protected set; }
        protected readonly int _rows = 4;
        protected readonly int _columns = 3;

        protected ItemRepository()
        {
            Items = new GameItem[_rows, _columns];
        }

        #endregion

        #region Methods

        public void ClearInventory()
        {
            Items = new GameItem[_rows, _columns];
        }

        public GameItem GetSlotItemInfo(int row, int column)
        {
            return Items[row, column];
        }

        public bool AddGameItem(GameItem gameItem)
        {

            foreach (var item in Items)
            {
                if (item == null)
                    continue;


                if (item.ItemId == gameItem.ItemId)
                {
                    if (item is ICountable countable)
                    {
                        countable.Amount += ((ICountable)gameItem).Amount;

                        if (countable.MaxAmount < countable.Amount)
                        {
                            int remainAmount = countable.Amount - countable.MaxAmount;
                            countable.Amount = countable.MaxAmount;
                            ((ICountable)gameItem).Amount = remainAmount;
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


            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (Items[i, j] == null)
                    {
                        Items[i, j] = gameItem;
                        OnInventoryItemChanged(Items);
                        return true;

                    }
                }
            }

            OnInventoryIsFull(new InventoryIsFullEventArgs());
            return false;

        }

        public void SwapItem(GameItem source, GameItem target)
        {
            GameItem temp = target;
            target = source;
            source = temp;

            OnInventoryItemChanged(Items);
        }

        public void RemoveItem(ItemID itemId, int amount)
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (Items[i, j].ItemId == itemId)
                    {
                        if (Items[i, j] is ICountable countable)
                        {
                            int temp = countable.Amount;
                            countable.Amount -= amount;

                            if (countable.Amount > 0)
                                break;

                            Items[i, j] = null;
                            amount -= temp;
                        }
                        else
                        {
                            if (amount == 0)
                                break;

                            Items[i, j] = null;

                            amount--;
                        }
                    }
                }
            }

            OnInventoryItemChanged(Items);

        }



        public int GetAmountForId(ItemID itemId)
        {
            int amount = 0;

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (Items[i, j].ItemId == itemId)
                    {
                        if (Items[i, j] is ICountable countable)
                        {
                            amount += countable.Amount;
                        }
                        else
                        {
                            amount++;
                        }
                    }
                }
            }



            return amount;

        }
        #endregion

        #region Events

        #region InventoryItemChanged
        public event EventHandler<InventoryItemChangedEventArgs> InventoryItemChanged;

        protected virtual void OnInventoryItemChanged(InventoryItemChangedEventArgs e)
        {
            if (InventoryItemChanged != null)
                InventoryItemChanged(this, e);
        }

        private InventoryItemChangedEventArgs OnInventoryItemChanged(GameItem[,] GameItems)
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

        public class InventoryItemChangedEventArgs : EventArgs
        {
            public GameItem[,] GameItems { get; set; }

            public InventoryItemChangedEventArgs()
            {
            }

            public InventoryItemChangedEventArgs(GameItem[,] gameItems)
            {
                GameItems = gameItems;
            }
        }

        #endregion

        #region InventoryIsFull
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

        #endregion
    }
}
