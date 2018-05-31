
using LandRushLibrary.Item;

namespace LandRushLibrary.ConcreteItem
{
    public abstract class GameItem<T> where T : ItemInfo
    {
        public T Info { get; set; }

    }
}
