using LandRushLibrary.Interfaces;
using LandRushLibrary.PlayerItemManagers;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Bow : EquipmentItem, IUpgradable
    {
        public int AttackPower { get; set; }
        public int Grade { get; set; }

        public override GameItem Clone()
        {
            Bow clone = new Bow();
            CloneCore(clone);

            clone.AttackPower = AttackPower;
            clone.Grade = Grade;

            return clone;
        }


    }
}
