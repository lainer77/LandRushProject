
using System.Security.Permissions;
using LandRushLibrary.Interfaces;
using LandRushLibrary.Units;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Potion : UseableItem
    { 
        public float RecorveyPoint { get; set; }

        public override GameItem Clone()
        {
            Potion clone = new Potion();
            CloneCore(clone);

            clone.RecorveyPoint = RecorveyPoint;


            return clone;
        }

        public override void UseItem()
        {
            Player.Instance.CurrentHp += ((int)(Player.Instance.MaxHp * RecorveyPoint));
        }
    }
}
