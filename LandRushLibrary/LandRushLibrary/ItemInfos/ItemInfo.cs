using LandRushLibrary.Repository;

namespace LandRushLibrary.ItemInfos
{
    public abstract class ItemInfo
    {
        public ItemId ItemId { get;  set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public string PrefabName { get; set; }

    }
}
