
using LandRushLibrary.Repository;
using System.Linq;

namespace LandRushLibrary.PlayerItemManagers
{
    public class Inventory : ItemRepository
    {
        private static Inventory _instance;

        public static Inventory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Inventory();

                return _instance;
            }
        }
    }
}
