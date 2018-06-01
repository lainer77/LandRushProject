using System.Collections.Generic;
using LandRushLibrary.Repository;
using LandRushLibrary.UnitInfos;

namespace LandRushLibrary.UnitInfos
{
    public class UnitInfoRepository
    {
        private static UnitInfoRepository _instance;
        private PlayerInfo _playerInfo;
        private Dictionary<int, MonsterInfo> _monsterInfoDictionary;

        public static UnitInfoRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnitInfoRepository();
                }

                return _instance;
            }
        }

        private UnitInfoRepository()
        {
            _monsterInfoDictionary = new Dictionary<int, MonsterInfo>();

            _playerInfo = new PlayerInfo
            {
                UnitId = UnitId.PLAYER,
                Name = "Player",
                Level = 1,
                Armor = 5,
                MaxHp = 30,
                PrefabName = "playerPrefab",
                MaxExp = 200,
                CurrentExp = 0

            };

            MonsterInfo orcInfo = new MonsterInfo
            {
                UnitId = UnitId.ORC,
                Name = "Orc",
                MonsterType = MonsterType.NORMAL,
                AttackPower = 10,
                PrefabName = "OrcPrefab",
                SlainExp = 20,
                MaxHp = 30,
                CurrentHp = 30
            };

            _monsterInfoDictionary.Add(orcInfo.UnitId, orcInfo);

            MonsterInfo orcLordInfo = new MonsterInfo
            {
                UnitId = UnitId.ORC_LORD,
                Name = "OrcLord",
                MonsterType = MonsterType.BOSS,
                AttackPower = 15,
                PrefabName = "OrcLordPrefab"
            };

            _monsterInfoDictionary.Add(orcLordInfo.UnitId, orcLordInfo);
        }

        public MonsterInfo GetMonsterInfo(int unitId)
        {
            return new MonsterInfo(_monsterInfoDictionary[unitId]);
        }

        public PlayerInfo GetPlayerInfo()
        {
            return _playerInfo;
            // return new PlayerInfo(_playerInfo);
        }

        //public GameObject SpawnUnitInfo()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
