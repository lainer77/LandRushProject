using LandRushLibrary.Interfaces;
using LandRushLibrary.PlayerItemManagers;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Sword : EquipmentItem, IUpgradable
    {
        public int AttackPower { get; set; }
        public int Grade { get; set; }

        public override GameItem Clone()
        {
            Sword clone = new Sword();
            CloneCore(clone);

            clone.AttackPower = AttackPower;
            clone.Grade = Grade;

            return clone;
        }


    }
}
