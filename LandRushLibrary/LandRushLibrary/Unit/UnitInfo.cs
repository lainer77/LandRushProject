

namespace LandRushLibrary.Unit
{
    public interface ICloneable<T>
    {
        T Clone();
    }

    public abstract class UnitInfo : ICloneable<UnitInfo>
    {
        protected UnitInfo()
        {

        }

        protected UnitInfo(UnitInfo origin)
        {
            UnitId = origin.UnitId;
            Name = string.Copy(origin.Name);
            PrefabName = string.Copy(origin.PrefabName);
            AttackPower = origin.AttackPower;
            Armor = origin.Armor;
            MaxHp = origin.MaxHp;
            CurrentHp = MaxHp;
            Speed = origin.Speed;
        }

        public int UnitId { get; set; }
        public string Name { get; set; }
        public string PrefabName { get; set; }
        public int AttackPower { get; set; }
        public int Armor { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public float Speed { get; set; }

        public abstract UnitInfo Clone();

    }
}
