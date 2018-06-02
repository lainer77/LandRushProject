using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class EquipmentItem : GameItem
    {
        public int Grade { get; set; }

        protected override void SetBasicCloneItem(GameItem clone)
        {
            ((EquipmentItem)clone).Grade = Grade;
            base.SetBasicCloneItem(clone);
        }
    }
}
