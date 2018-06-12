using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class GameItem
    {
        public ItemID ItemId { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public string PrefabName { get; set; }
        public ItemType Type { get; set; }
        [JsonProperty]
        public int MaxAmount { get; protected set; }
        [JsonIgnore]
        public int Amount { get; set; }

        public int AddAmount(int amount)
        {
            int remainAmount = 0;

            if ( Amount + amount > MaxAmount )
            {
                Amount = MaxAmount;
                remainAmount = Amount + amount - MaxAmount;
            }
            else
            {
                Amount += amount;
            }

            return remainAmount;

        }


        public abstract GameItem Clone();
        
        protected virtual void CloneCore(GameItem clone)
        {
            clone.ItemId = ItemId;
            clone.Name = Name;
            clone.IconName = IconName;
            clone.PrefabName = PrefabName;
            clone.Type = Type;
            clone.MaxAmount = MaxAmount;
            clone.Amount = 1;
    
        }

    }
}
