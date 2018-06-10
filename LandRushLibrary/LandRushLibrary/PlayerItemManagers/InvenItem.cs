
using LandRushLibrary.Items;

namespace LandRushLibrary.PlayerItemManagers
{
    public class InvenItem
    {
        public InvenItem(GameItem item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public GameItem Item { get; set; }
        public int Amount { get; set; }
    }
}
