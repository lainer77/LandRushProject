using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class ConsumableItem : GameItem
    {
        public abstract void UseItem();
    }
}
