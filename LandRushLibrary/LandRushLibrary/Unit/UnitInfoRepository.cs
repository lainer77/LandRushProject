using System;
using System.Collections.Generic;
using LandRushLibrary.Repository;

namespace LandRushLibrary.Unit
{
    public class UnitInfoRepository
    {
        private static UnitInfoRepository _instance;
        private PlayerInfo _playerInfo;
        private Dictionary<int, MonsterInfo> _MonsterInfoInfos;

        public static UnitInfoRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnitInfoRepository();
                    _instance.InitUnitInfoInfo();
                }

                return _instance;
            }
        }

        //public UnitInfo GetUnitInfoInfo(int unitId)
        //{
        //    return _MonsterInfoInfos[unitId];
        //}

        public void InitUnitInfoInfo()
        {
            _MonsterInfoInfos = new Dictionary<int, MonsterInfo>();

            PlayerInfo playerInfo = new PlayerInfo
            {
                UnitId =  UnitId.PLAYER,
                Name = "Player",
                Level = 1,
                Armor = 5,
                MaxHp = 30,
                PrefabName = "playerPrefab",
                MaxExp = 200,
                CurrentExp = 0

            };
            _playerInfo = playerInfo;

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

            _MonsterInfoInfos.Add(orcInfo.UnitId, orcInfo);            

            MonsterInfo orcLordInfo = new MonsterInfo
            {
                UnitId = UnitId.ORC_LORD,
                Name = "OrcLord",
                MonsterType = MonsterType.BOSS,
                AttackPower = 15,
                PrefabName = "OrcLordPrefab"
            };

            _MonsterInfoInfos.Add(orcLordInfo.UnitId, orcLordInfo);

        }

        public MonsterInfo GetMonsterInfo(int unitId)
        {
            return new MonsterInfo(_MonsterInfoInfos[unitId]);
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
