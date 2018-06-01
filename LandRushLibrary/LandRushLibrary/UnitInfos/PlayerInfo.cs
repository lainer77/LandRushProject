namespace LandRushLibrary.UnitInfos

{
    public class PlayerInfo : UnitInfo
    {
        public PlayerInfo()
        { }

        public PlayerInfo(int id, string name, int armor, int maxHp, string prefabName)
        {
            UnitId = id;
            Name = name;
            Armor = armor;
            MaxHp = maxHp;
            CurrentHp = MaxHp;
            PrefabName = prefabName;
        }

        public PlayerInfo(PlayerInfo origin) : base(origin)
        {
            Level = origin.Level;
            CurrentExp = origin.CurrentExp;
        }


        public int Level { get; set; }
        public int CurrentExp { get; set; }
        public int MaxExp { get; set; }

        public override UnitInfo.UnitInfo Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
