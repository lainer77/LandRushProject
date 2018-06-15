using System.Collections.Generic;
using System.IO;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using Newtonsoft.Json;

namespace LandRushLibrary.Utilities
{
    internal class MonsterSerializer
    {
        public const string MonstersFilePath = "Assets/Assets//Json//monster.json";

        #region singleton
        private static MonsterSerializer _instance;

        public static MonsterSerializer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MonsterSerializer();
                return _instance;
            }
        }

        private MonsterSerializer()
        {

        }
        #endregion

        public void Serialize(Dictionary<MonsterID, Monster> dictionary)
        {
            var json = JsonConvert.SerializeObject(dictionary);
            File.WriteAllText(MonstersFilePath, json);
        }

        public Dictionary<MonsterID, Monster> Deseriailize()
        {
            var json = File.ReadAllText(MonstersFilePath);
            return JsonConvert.DeserializeObject<Dictionary<MonsterID, Monster>>(json);
        }
    }
}
