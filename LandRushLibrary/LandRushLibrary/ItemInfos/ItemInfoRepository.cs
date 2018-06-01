using System.Collections.Generic;
using LandRushLibrary.Repository;

namespace LandRushLibrary.ItemInfos
{
    public class ItemInfoRepository
    {
        private static ItemInfoRepository _instance;

        public static ItemInfoRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemInfoRepository();
                }

                return _instance;
            }
        }


        private ItemInfoRepository()
        {
            _itemInfoDictionary = new Dictionary<int, ItemInfo>();

            SwordInfo oldSwordInfo = new SwordInfo
            {
                ItemId = ItemId.OLD_SWORD,

            };
            ShieldInfo oldShieldInfo = new ShieldInfo
            {
                ItemId = ItemId.OLD_SHIELD
            };
            BowInfo oldBowInfo = new BowInfo
            {
                ItemId = ItemId.OLD_BOW
            };
        }

        private Dictionary<int, ItemInfo> _itemInfoDictionary;

        public ItemInfo GetItemInfo(int itemId)
        {
            return _itemInfoDictionary[itemId];
        }
    }
}
