
using System;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Utilities
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ParsedItem
    {
        public Sword[] swords;
        public Bow[] bows;
        public Shield[] shields;
        public Potion[] potions;
        public Arrow[] arrows;
        public IngredientItem[] ingredients;

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
