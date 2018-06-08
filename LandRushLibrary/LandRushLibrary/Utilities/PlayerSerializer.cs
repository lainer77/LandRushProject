
using LandRushLibrary.Units;
using Newtonsoft.Json;
using System.IO;

namespace LandRushLibrary.Utilities
{
    internal class PlayerSerializer
    {
        private static PlayerSerializer _instance;
        public static PlayerSerializer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerSerializer();

                return _instance;
            }
        }

        private const string PlayerFilePath = "../Assets//Json//player.json";

        public void Serialize()
        {
            var json = JsonConvert.SerializeObject(Player.Instance);
            File.WriteAllText(PlayerFilePath, json);
        }

        public Player DeSerialize()
        {
            var json = File.ReadAllText(PlayerFilePath);
            return JsonConvert.DeserializeObject<Player>(json);


        }


    }
}
