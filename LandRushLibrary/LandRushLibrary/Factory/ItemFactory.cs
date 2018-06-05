
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using LandRushLibrary.Utilities;
using System.Linq;

namespace LandRushLibrary.Factory
{
    public class ItemFactory
    {
        private static ItemFactory _instace;

        public static ItemFactory Instance
        {
            get
            {
                if (_instace == null)
                    _instace = new ItemFactory();

                return _instace;
            }
        }

        private readonly Dictionary<ItemID, GameItem> _items;

        private ItemFactory()
        {
            _items = ItemSerializer.Instance.Deseriailize();
        }

        public GameItem Create(ItemID itemId)
        {
            GameItem clone = _items[itemId].Clone();

            return clone;
        }

        public T Create<T>(ItemID itemId) where T : GameItem
        {
            GameItem clone = _items[itemId].Clone();

            return clone as T;
        }

        public ItemID FindItemId<T> (Func<T, bool> predicate) where T : GameItem
        {
            var items = from x in _items
                        select x.Value as T;

            var item = items.FirstOrDefault(predicate);

            return item.ItemId;

        }
    }
}
