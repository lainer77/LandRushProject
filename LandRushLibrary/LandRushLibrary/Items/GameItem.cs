
using LandRushLibrary.ItemInfos;

namespace LandRushLibrary.Items
{
    public abstract class GameItem<T> where T : ItemInfo
    {
        public T Info { get; set; }

    }
}
