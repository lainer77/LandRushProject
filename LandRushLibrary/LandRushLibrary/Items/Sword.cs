using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Sword : EquipmentItem
    {
        public int AttackPower { get; set; }

    }
}
