using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandRushLibrary.ConcreteUnit;
using LandRushLibrary.Unit;

namespace LandRushLibrary.Repository
{
    public class MonsterFactory
    {
        #region singleton
        private static MonsterFactory _instance;

        public static MonsterFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MonsterFactory();
                return _instance;
            }
        }

        private MonsterFactory()
        {
        }
        #endregion

        public Monster Create(int monsterId)
        {
            MonsterInfo monster = UnitInfoRepository.Instance.GetMonsterInfo(monsterId);

            return monster;
        }

        public Monster Create(UnitInfo unitInfo)
        {
            Monster monster = new Monster();

            monster.HP = unitInfo.MaxHp;
            monster.Speed = unitInfo.Speed;

            return monster;
        }
    }
}
