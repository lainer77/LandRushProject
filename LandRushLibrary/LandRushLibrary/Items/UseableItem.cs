using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract  class UseableItem : GameItem
    {
        public abstract void UseItem();
    }
}
