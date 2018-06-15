using LandRushLibrary.Items;

namespace LandRushLibrary.PlayerItemManagers
{
    public class PlayerInventory : ItemRepository
    {
        #region Singleton
        private static PlayerInventory _instance;

        public static PlayerInventory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerInventory();

                return _instance;
            }
        }


        #endregion
    }
}
