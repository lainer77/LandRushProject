using LandRushLibrary.Items;

namespace LandRushLibrary.PlayerItemManagers
{
    public class Inventory : ItemRepository
    {
        #region Singleton
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


        #endregion
    }
}
