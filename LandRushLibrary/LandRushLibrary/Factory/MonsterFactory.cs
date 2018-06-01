
using LandRushLibrary.ConcreteUnit;
using LandRushLibrary.UnitInfos;

namespace LandRushLibrary.Factory
{
    public class MonsterFactory : Factory<Monster>
    {
        private MonsterFactory _instace;

        public MonsterFactory Instance
        {
            get
            {
                if(_instace == null)
                    _instace = new MonsterFactory();

                return _instace;
            }
        }

        private MonsterFactory()
        {

        }

        public override Monster Create(int monsterId)
        {
            MonsterInfo monsterInfo = UnitInfoRepository.Instance.GetMonsterInfo(monsterId);
            Monster monster = new Monster();

            monster.UnitId = monsterInfo.UnitId;
            monster.Name = monsterInfo.Name;
            monster.AttackPower = monsterInfo.AttackPower;
            monster.Armor = monsterInfo.Armor;
            monster.MaxHp = monsterInfo.MaxHp;
            monster.CurrentHp = monsterInfo.CurrentHp;
            monster.Speed= monsterInfo.Speed;
            monster.SlainExp = monsterInfo.SlainExp;
            monster.MonsterType = monsterInfo.MonsterType;

            return monster;
        }
    }
}
