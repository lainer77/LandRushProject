using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Bow : EquipmentItem
    {
        public int AttackPower { get; set; }

        public override GameItem Clone()
        {
            Bow clone = new Bow();
            SetBasicCloneItem(clone);

            clone.AttackPower = AttackPower;

            return clone;
        }
    }
}
