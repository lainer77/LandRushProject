
using LandRushLibrary.Repository;

namespace LandRushLibrary.Drop
{
    public class DropInfo
    {
        public ItemID ItemId { get; private set; }
        public int Amount { get; private set; }

        public DropInfo( ItemID itemId, int amount)
        {
            ItemId = itemId;
            Amount = amount;
        }
    }
}
