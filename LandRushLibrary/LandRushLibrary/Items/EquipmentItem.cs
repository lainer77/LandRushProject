using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class EquipmentItem : GameItem
    {

    }
}
