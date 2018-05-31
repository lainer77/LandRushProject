using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandRushLibrary.Item
{
    public abstract class ItemInfo
    {
        public int ItemId { get;  set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public string PrefabName { get; set; }

    }
}
