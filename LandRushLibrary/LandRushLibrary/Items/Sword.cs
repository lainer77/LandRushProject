using LandRushLibrary.ItemInfos;
using LandRushLibrary.Units;

namespace LandRushLibrary.Items
{
    public class Sword : EquipmentItem<SwordInfo>
    {
        public Sword(int itemId)
        {
            Info = (SwordInfo)ItemInfoRepository.Instance.GetItemInfo(itemId);
        }

    }
}
