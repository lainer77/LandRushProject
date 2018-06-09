
using LandRushLibrary.Repository;

namespace LandRushLibrary.Drop
{
    public class DroppedItems
    {
        public ItemID ItemId { get; private set; }
        public int Amount { get; private set; }

        public DroppedItems( ItemID itemId, int amount)
        {
            ItemId = itemId;
            Amount = amount;
        }
    }
}
