using System;
using System.Runtime.InteropServices;
using LandRushLibrary.Interfaces;
using LandRushLibrary.PlayerItemManagers;
using Newtonsoft.Json;

namespace LandRushLibrary.Items 
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Arrow : EquipmentItem
    {
        public override GameItem Clone()
        {
            Arrow clone = new Arrow();
            CloneCore(clone);

            return clone;
        }


    }
}
