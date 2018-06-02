
namespace LandRushLibrary.Items
{
    public class Shield : EquipmentItem
    {
        public int Armor { get; set; }

        public override GameItem Clone()
        {
            Shield clone = new Shield();
            SetBasicCloneItem(clone);

            clone.Armor = Armor;

            return clone;
        }
    }
}
