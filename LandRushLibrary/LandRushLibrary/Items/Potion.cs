
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Potion : ConsumpiveItem
    {
        public int RecorveyPoint { get; set; }
    }
}
