using Newtonsoft.Json;

namespace LandRushLibrary.Interfaces
{
    [JsonObject(MemberSerialization.OptOut)]
    public interface ICountable
    {
        int Amount { get; set; }
        int MaxAmount { get; }

    }
}
