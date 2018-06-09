using System.Collections.Generic;
using System.IO;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Utilities
{
    internal class ItemSerializer
    {
       private const string ItemsFilePath = "Assets//Json//items.json";

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

        public void Serialize(Dictionary<ItemID, GameItem> gameItems)
        {

           string json = JsonConvert.SerializeObject(gameItems);
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
