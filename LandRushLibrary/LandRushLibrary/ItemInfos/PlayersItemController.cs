using System.Collections.Generic;
using LandRushLibrary.Items;

namespace LandRushLibrary.ItemInfos
{
    /// <summary>
    /// 작업자 : 남민우
    /// 작업시간 :
    /// 내용 : 플레이어가 가진 아이템 컨트롤함 
    /// </summary>
    public class PlayersItemController
    {
        private PlayersItemController _instance;

        public PlayersItemController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayersItemController();

                return _instance;
            }
        }

        private int _currentEquipmentPare;
        public List<EquipmentPare> EquipmentPares;

        public struct EquipmentPare
        {
            public EquipmentItem<EquipmentItemInfo> LeftEquipment { get; set; }
            public EquipmentItem<EquipmentItemInfo> RightEquipment { get; set; }
        }
        
    }
}
