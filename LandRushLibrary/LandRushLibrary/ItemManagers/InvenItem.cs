
using LandRushLibrary.Items;

namespace LandRushLibrary.ItemManagers
{
    public class InvenItem
    {
        public InvenItem(GameItem item, int ammount)
        {
            Item = item;
            Ammount = ammount;
        }

        public GameItem Item { get; set; }
        public int Ammount { get; set; }
    }
}
