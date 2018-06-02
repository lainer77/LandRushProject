using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Sword : EquipmentItem
    {
        public int AttackPower { get; set; }

        public override GameItem Clone()
        {
            Sword clone = new Sword();
            SetBasicCloneItem(clone);

            clone.AttackPower = AttackPower;

            return clone;
        }
    }
}
