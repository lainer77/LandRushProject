
using Newtonsoft.Json;

namespace LandRushLibrary.Items 
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Arrow : GameItem
    {
        public override GameItem Clone()
        {
            Arrow clone = new Arrow();
            SetBasicCloneItem(clone);

            return clone;
        }

    }
}
