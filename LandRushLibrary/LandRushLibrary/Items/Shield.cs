
using LandRushLibrary.Interfaces;

namespace LandRushLibrary.Items
{
    public class Shield : EquipmentItem, IUpgradable
    {
        public int Armor { get; set; }
        public int Grade { get; set; }
        public override GameItem Clone()
        {
            Shield clone = new Shield();
            CloneCore(clone);

            clone.Grade = Grade;
            clone.Armor = Armor;

            return clone;
        }

    }
}
