using Newtonsoft.Json;

namespace LandRushLibrary.Interfaces
{
    [JsonObject(MemberSerialization.OptOut)]
    public interface IUpgradable
    {
        int Grade { get; set; }
    }
}
