namespace LandRushLibrary.UnitInfos
{
    public class MonsterInfo : UnitInfo
    {
        public MonsterInfo()
        { }

        public MonsterInfo(int id, string name, int type, int attackPower, string prefabName)
        {
            UnitId = id;
            Name = name;
            MonsterType = type;
            AttackPower = attackPower;
            PrefabName = prefabName;
        }

        public MonsterInfo(MonsterInfo origin) : base(origin)
        {
            SlainExp = origin.SlainExp;
            MonsterType = origin.MonsterType;
        }

        public int SlainExp { get; set; }
        public int MonsterType { get; set; }

        public override UnitInfo Clone()
        {
            UnitInfo monsterInfo = new MonsterInfo();

            monsterInfo.UnitId = UnitId;
            monsterInfo.Name = Name;
            monsterInfo.PrefabName = PrefabName;
            monsterInfo.AttackPower = AttackPower;
            monsterInfo.Armor = Armor;
            monsterInfo.MaxHp = MaxHp;
            monsterInfo.CurrentHp = CurrentHp;
            monsterInfo.Speed = Speed;

            return monsterInfo;
        }
    }
}
