
using LandRushLibrary.Interfaces;
using LandRushLibrary.PlayerItemManagers;
using Newtonsoft.Json;

namespace LandRushLibrary.Items 
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Arrow : EquipmentItem, ICountable
    {
        [JsonIgnore]
        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public override GameItem Clone()
        {
            Arrow clone = new Arrow();
            CloneCore(clone);

            clone.Amount = 1;

            return clone;
        }


    }
}
