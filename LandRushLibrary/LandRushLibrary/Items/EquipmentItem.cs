using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class EquipmentItem : GameItem
    {
        public int Grade { get; set; }
    }
}
