using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandRushLibrary.Repository;

namespace LandRushLibrary.Item
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
                    _instance.InitItemInfos();
                }

                return _instance;
            }
        }

        private Dictionary<int, ItemInfo> _itemInfos;

        private void InitItemInfos()
        {
            _itemInfos = new Dictionary<int, ItemInfo>();

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

        public ItemInfo GetItemInfo(int itemId)
        {
            return _itemInfos[itemId];
        }
    }
}
