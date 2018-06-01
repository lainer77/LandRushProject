using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Units
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Monster : Unit, IAttackable
    {
        public int SlainExp { get; set; }
        public MonsterGrade MonsterType { get; set; }
        public string PrefabName { get; set; }

        public override void GetDamage(int damage)
        {
            CurrentHp -= damage;
            if(CurrentHp <= 0)
                OnDead(new DeadEventArgs(this));
        }

        public event Predicate<Unit> CorrectTargetUnit;
        public void Attack(Unit attakedUnit, bool guard)
        {
            if (CorrectTargetUnit != null && CorrectTargetUnit(attakedUnit))
                return;

            int damage = AttackPower;

            OnCalculatedRandomDamage(AttackPower);

            int armor = attakedUnit.Armor;

            if (guard && (attakedUnit is Player player))
            {
                armor += player.ShieldArmor;
            }

            damage -= armor;
            if (damage < 0)
                damage = 0;

            attakedUnit.GetDamage(damage);
        }
        public event EventHandler<CalculatedRandomDamageEventArgs> CalculatedRandomDamage;

        protected virtual void OnCalculatedRandomDamage(CalculatedRandomDamageEventArgs e)
        {
            if (CalculatedRandomDamage != null)
                CalculatedRandomDamage(this, e);
        }

        private CalculatedRandomDamageEventArgs OnCalculatedRandomDamage(int attackPower)
        {
            CalculatedRandomDamageEventArgs args = new CalculatedRandomDamageEventArgs(attackPower);
            OnCalculatedRandomDamage(args);

            return args;
        }

        public Monster Clone()
        {
            Monster clone = new Monster();

            clone.MonsterId = MonsterId;
            clone.Name = Name;
            clone.AttackPower = AttackPower;
            clone.Armor = Armor;
            clone.MaxHp = MaxHp;
            clone.CurrentHp = CurrentHp;
            clone.Speed = Speed;
            clone.SlainExp = SlainExp;
            clone.PrefabName = PrefabName;
            clone.MonsterType = MonsterType;
 
            return clone;
        }
    }

}
