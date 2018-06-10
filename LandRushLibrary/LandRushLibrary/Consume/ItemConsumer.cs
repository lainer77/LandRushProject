
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using System;

namespace LandRushLibrary.Consume
{
    public class ItemConsumer
    {
        protected static ItemConsumer _instance;
        public static ItemConsumer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemConsumer();

                return _instance;
            }
        }

        private ItemConsumer()
        {

        }

        public void ConsumeItem(ConsumableItem consumedItem)
        {
            int amount = Inventory.Instance.GetAmountForId(consumedItem.ItemId);

            if (amount <= 0)
            {
                OnItemConsumeFailed(new ItemConsumeFailedEventArgs(consumedItem.ItemId, true));
                return;
            }

            consumedItem.UseItem();

            Inventory.Instance.RemoveItem(consumedItem.ItemId, 1);

            OnItemConsumed(new ItemConsumedEventArgs(consumedItem.ItemId));

        }

        #region ItemConsumed event things for C# 3.0
        public event EventHandler<ItemConsumedEventArgs> ItemConsumed;

        protected virtual void OnItemConsumed(ItemConsumedEventArgs e)
        {
            if (ItemConsumed != null)
                ItemConsumed(this, e);
        }

        private ItemConsumedEventArgs OnItemConsumed(ItemID itemId, bool notEnoughItem)
        {
            ItemConsumedEventArgs args = new ItemConsumedEventArgs(itemId);
            OnItemConsumed(args);

            return args;
        }

        private ItemConsumedEventArgs OnItemConsumedForOut()
        {
            ItemConsumedEventArgs args = new ItemConsumedEventArgs();
            OnItemConsumed(args);

            return args;
        }


        #endregion

        #region ItemConsumeFailed event things for C# 3.0
        public event EventHandler<ItemConsumeFailedEventArgs> ItemConsumeFailed;

        protected virtual void OnItemConsumeFailed(ItemConsumeFailedEventArgs e)
        {
            if (ItemConsumeFailed != null)
                ItemConsumeFailed(this, e);
        }

        private ItemConsumeFailedEventArgs OnItemConsumeFailed(ItemID itemId, bool notEnoughItem)
        {
            ItemConsumeFailedEventArgs args = new ItemConsumeFailedEventArgs(itemId, notEnoughItem);
            OnItemConsumeFailed(args);

            return args;
        }

        private ItemConsumeFailedEventArgs OnItemConsumeFailedForOut()
        {
            ItemConsumeFailedEventArgs args = new ItemConsumeFailedEventArgs();
            OnItemConsumeFailed(args);

            return args;
        }


        #endregion
    }

    public class ItemConsumedEventArgs : EventArgs
    {
        public ItemID ItemId { get; set; }

        public ItemConsumedEventArgs()
        {
        }

        public ItemConsumedEventArgs(ItemID itemId)
        {
            ItemId = itemId;
        }
    }

    public class ItemConsumeFailedEventArgs : EventArgs
    {
        public ItemID ItemId { get; set; }
        public bool NotEnoughItem { get; set; }

        public ItemConsumeFailedEventArgs()
        {
        }

        public ItemConsumeFailedEventArgs(ItemID itemId, bool notEnoughItem)
        {
            ItemId = itemId;
            NotEnoughItem = notEnoughItem;
        }
    }
}
