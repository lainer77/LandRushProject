
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Potion : ConsumpiveItem
    {
        public int RecorveyPoint { get; set; }

        public override GameItem Clone()
        {
            Potion clone = new Potion();
            SetBasicCloneItem(clone);

            clone.RecorveyPoint = RecorveyPoint;

            return clone;
        }
    }
}
