using System.Collections.Generic;
using System.IO;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Utilities
{
    public class ItemSerializer
    {
       private const string ItemsFilePath = "..//Assets//Json//items.json";

        private static ItemSerializer _instance;

        public static ItemSerializer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemSerializer();

                return _instance;
            }
        }

        private ItemSerializer()
        {

        }

        public void Serialize()
        {
            Dictionary<ItemID, GameItem> dictionary = new Dictionary<ItemID, GameItem>();

            Sword sword = new Sword
            {
                ItemId = ItemID.OLD_SWORD,
                Name = "OldSword",
                PrefabName = "OldSword",
                AttackPower = 10,
                IconName = "OldSword",
                Type = ItemType.Sword
            };
            dictionary.Add(sword.ItemId, sword);

            Bow bow = new Bow
            {
                ItemId = ItemID.OLD_BOW,
                Name = "OldBow",
                PrefabName = "OldBow",
                AttackPower = 10,
                IconName = "OldBow",
                Type = ItemType.Bow

            };
            dictionary.Add(bow.ItemId, bow);

            Potion potion = new Potion
            {
                ItemId = ItemID.POTION,
                Name = "Potion",
                PrefabName = "Potion",
                IconName = "Potion",
                Type = ItemType.Potion
            };
            dictionary.Add(potion.ItemId, potion);


            Arrow arrow = new Arrow
            {
                ItemId = ItemID.ARROW,
                Name = "Arrow",
                PrefabName = "Arrow",
                IconName = "Arrow",
                Type = ItemType.Arrow
            };
            dictionary.Add(arrow.ItemId, arrow);

            IngredientItem stone = new IngredientItem
            {
                ItemId = ItemID.STONE,
                Name = "Stone",
                PrefabName = "StonePrefab",
                IconName = "StoneIcon",
                Type = ItemType.Ingredient
            };
            dictionary.Add(stone.ItemId, stone);

            string json = JsonConvert.SerializeObject(dictionary);
            File.WriteAllText(ItemsFilePath, json);
        }

        public Dictionary<ItemID, GameItem> Deseriailize()
        {
            var json = File.ReadAllText(ItemsFilePath);
            ParsedItem parsedItem = JsonConvert.DeserializeObject<ParsedItem>(json);
            return parsedItem.GetItemDictionary();
        }


    }
}
