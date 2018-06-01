using System.Collections.Generic;
using System.IO;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using Newtonsoft.Json;

namespace LandRushLibrary.Utilities
{
    public class UnitSerializer
    {
        public const string MonstersFilePath = "c:\\monsters.json";

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

        public void Serialize()
        {
            var dictionary = new Dictionary<MonsterID, Monster>();

            Monster orcInfo = new Monster
                                  {
                                      MonsterId = MonsterID.ORC,
                                      Name = "Orc",
                                      MonsterType = MonsterGrade.NORMAL,
                                      AttackPower = 10,
                                      PrefabName = "OrcPrefab",
                                      SlainExp = 20,
                                      MaxHp = 30,
                                      CurrentHp = 30
                                  };

            dictionary.Add(orcInfo.MonsterId, orcInfo);

            Monster orcLordInfo = new Monster
                                      {
                                          MonsterId = MonsterID.ORC_LORD,
                                          Name = "OrcLord",
                                          MonsterType = MonsterGrade.BOSS,
                                          AttackPower = 15,
                                          PrefabName = "OrcLordPrefab"
                                      };

            dictionary.Add(orcLordInfo.MonsterId, orcLordInfo);

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
