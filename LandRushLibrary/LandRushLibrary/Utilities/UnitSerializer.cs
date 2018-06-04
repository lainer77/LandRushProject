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
            Dictionary<MonsterID, Monster> dictionry = new Dictionary<MonsterID, Monster>();

            Monster orc = new Monster
            {
                MonsterId = MonsterID.ORC,
                Name = "Orc",
                AttackPower = 10,
                Armor = 5,
                Speed = 10.0f,
                MaxHp = 30,
                CurrentHp = 30,
                PrefabName = "OrcPrefab",
                SlainExp = 10,
                MonsterGrade = MonsterGrade.NORMAL

            };

            dictionry.Add(orc.MonsterId, orc);

            Monster orcLord = new Monster
            {
                MonsterId = MonsterID.ORC_LORD,
                Name = "OrcLord",
                AttackPower = 20,
                Armor = 8,
                Speed = 15.0f,
                MaxHp = 60,
                CurrentHp = 60,
                PrefabName = "OrcLordPrefab",
                SlainExp = 30,
                MonsterGrade = MonsterGrade.BOSS

            };

            dictionry.Add(orcLord.MonsterId, orc);


            Serialize(dictionry);
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
