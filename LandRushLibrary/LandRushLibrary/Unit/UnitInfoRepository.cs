using System;
using System.Collections.Generic;
using LandRushLibrary.Repository;

namespace LandRushLibrary.Unit
{
    public class UnitInfoRepository
    {
        #region singleton
        private static UnitInfoRepository _instance;

        public static UnitInfoRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UnitInfoRepository();
                return _instance;
            }
        }

        private UnitInfoRepository()
        {
            _monsterInfoDictionary = new Dictionary<int, MonsterInfo>();

            PlayerInfo = new PlayerInfo
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
        #endregion


        private Dictionary<int, MonsterInfo> _monsterInfoDictionary;

        //public UnitInfo GetUnitInfoInfo(int unitId)
        //{
        //    return _monsterInfoDictionary[unitId];
        //}

        public MonsterInfo GetMonsterInfo(int unitId)
        {
            return (MonsterInfo) _monsterInfoDictionary[unitId].Clone();
        }

        public MonsterInfo this[int unitId]
        {
            get
            {
                return (MonsterInfo) _monsterInfoDictionary[unitId].Clone();
            }
        }

        public PlayerInfo PlayerInfo { get; }

        //public GameObject SpawnUnitInfo()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
