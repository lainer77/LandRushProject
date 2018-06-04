using System.Collections.Generic;
using System.IO;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using Newtonsoft.Json;

namespace LandRushLibrary.Utilities
{
    public class UnitSerializer
    {
        public const string MonstersFilePath = "../Assets//Json//monster.json";

        #region singleton
        private static UnitSerializer _instance;

        public static UnitSerializer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UnitSerializer();
                return _instance;
            }
        }

        private UnitSerializer()
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
