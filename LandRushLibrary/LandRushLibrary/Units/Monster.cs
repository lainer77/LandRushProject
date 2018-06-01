using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Repository;

namespace LandRushLibrary.Units
{
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

        public void Attack(Unit attakedUnit, bool guard = false)
        {
            int damage = AttackPower;

            OnCalculatedRandomDamage(new CalculatedRandomDamageEventArgs(damage));

            damage -= attakedUnit.Armor;

            if (guard == true && (attakedUnit is Player))
                damage -= ((Player) attakedUnit).ShieldArmor;

            if (damage < 0)
                damage = 0;

            attakedUnit.GetDamage(damage);
        }


        public event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;

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
            clone.Name = Name;
            clone.AttackPower = AttackPower;

            return clone;
        }
    }

}
