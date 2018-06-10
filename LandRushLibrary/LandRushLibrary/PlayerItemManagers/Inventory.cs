
using LandRushLibrary.Repository;
using System.Linq;

namespace LandRushLibrary.PlayerItemManagers
{
    public class Inventory : ItemRepository<Inventory>
    {
        public int GetAmountForId(ItemID itemId)
        {
            var amount = (from x in Items
                          where x.Item.ItemId == itemId
                          select x.Amount).ToList();

            return amount.Sum();
            
        }
    }
}
