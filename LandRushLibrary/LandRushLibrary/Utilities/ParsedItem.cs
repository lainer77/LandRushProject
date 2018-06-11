using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Utilities
{
    [JsonObject(MemberSerialization.OptOut)]
    internal class ParsedItem
    {
        public List<Sword> swords;
        public List<Bow> bows;
        public List<Shield> shields;
        public List<Potion> potions;
        public List<Arrow> arrows;
        public List<IngredientItem> ingredients;

        public Dictionary<ItemID, GameItem> GetItemDictionary()
        {
            Dictionary<ItemID, GameItem> dictionary = new Dictionary<ItemID, GameItem>();

            foreach (var item in swords)
            {
                dictionary.Add(item.ItemId, item);
            }

            foreach (var item in shields)
            {
                dictionary.Add(item.ItemId, item);
            }

            foreach (var item in bows)
            {
                dictionary.Add(item.ItemId, item);
            }

            foreach (var item in arrows)
            {
                dictionary.Add(item.ItemId, item);
            }

            foreach (var item in potions)
            {
                dictionary.Add(item.ItemId, item);
            }

            foreach (var item in ingredients)
            {
                dictionary.Add(item.ItemId, item);
            }

            return dictionary;
        }
    }
}
