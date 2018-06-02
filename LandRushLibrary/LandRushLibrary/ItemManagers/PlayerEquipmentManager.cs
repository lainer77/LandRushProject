using LandRushLibrary.Items;
using System.Collections.Generic;

namespace LandRushLibrary.ItemManagers
{
    public class PlayerEquipmentManager
    {
        private static PlayerEquipmentManager _instance;
        public static PlayerEquipmentManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerEquipmentManager();

                return _instance;
            }
        }

        private PlayerEquipmentManager()
        {
            Equipments = new List<EquipmentItem>();
        }

        public List<EquipmentItem> Equipments { get; set; }
        private EquipmentPair _firstPair;
        private EquipmentPair _secondPair;

        public void SetEquipment(int slotNum, EquipmentItem equipment)
        {
            Equipments[slotNum] = equipment;
        }

        private class EquipmentPair
        {
            EquipmentItem LeftEquipment { get; set; } 
            EquipmentItem RightEquipment { get; set; } 
        }
    }


}
