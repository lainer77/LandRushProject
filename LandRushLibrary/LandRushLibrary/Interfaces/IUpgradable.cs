using Newtonsoft.Json;

namespace LandRushLibrary.Interfaces
{
    [JsonObject(MemberSerialization.OptOut)]
    public interface IUpgradable
    {
        public int Grade { get; set; }
    }
}
