using LandRushLibrary.Repository;
using System.Collections.Generic;

namespace LandRushLibrary.Drop
{
    internal class DropList
    {
        public ItemID ItemId { get; private set; }
        public float Rate { get; private set; }
        public int Amount { get; private set; }

        public DropList(ItemID itemId, float rate, int amount)
        {
            ItemId = itemId;
            Amount = amount;
            Rate = rate;
        }

        public DropList()
        {

        }

    }
}
