
using LandRushLibrary.Units;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Potion : ConsumableItem
    {
        public float RecorveyPoint { get; set; }

        public override GameItem Clone()
        {
            Potion clone = new Potion();
            SetBasicCloneItem(clone);

            clone.RecorveyPoint = RecorveyPoint;

            return clone;
        }

        public override void UseItem()
        {
            Player.Instance.CurrentHp += ( (int)(Player.Instance.MaxHp * RecorveyPoint) );
        }
    }
}
