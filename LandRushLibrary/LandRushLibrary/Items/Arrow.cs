
using LandRushLibrary.Interfaces;
using Newtonsoft.Json;

namespace LandRushLibrary.Items 
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Arrow : EquipmentItem, ICountable
    {

        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public override GameItem Clone()
        {
            Arrow clone = new Arrow();
            CloneCore(clone);

            return clone;
        }

    }
}
