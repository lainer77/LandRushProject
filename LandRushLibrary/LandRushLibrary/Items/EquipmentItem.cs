using System;
using LandRushLibrary.Interfaces;
using LandRushLibrary.PlayerItemManagers;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class EquipmentItem : GameItem, ICountable
    {
        [JsonIgnore]
        public int Amount { get; set; }
        public int MaxAmount { get; set; }
    }


}
