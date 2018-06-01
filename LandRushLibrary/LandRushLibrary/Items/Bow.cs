using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Bow : EquipmentItem
    {
        public int AttackPower { get; set; }
    }
}
