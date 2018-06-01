
using System;
using LandRushLibrary.Combat;
using LandRushLibrary.ConcreteUnit;
using LandRushLibrary.Item;
using LandRushLibrary.UnitInfo;
using LandRushLibrary.UnitInfos;

namespace LandRushLibrary.ConcreteItem
{
    public class Sword : EquipmentItem<SwordInfo>
    {
        private PlayerInfo _playerInfo;

        public Sword(int itemId, PlayerInfo playerInfo)
        {
            Info = (SwordInfo)ItemInfoRepository.Instance.GetItemInfo(itemId);
            _playerInfo = playerInfo;
        }

    }
}
