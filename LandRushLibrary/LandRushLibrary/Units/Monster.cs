﻿using System;
using System.Collections.Generic;
using LandRushLibrary.Combat;
using LandRushLibrary.Drop;
using LandRushLibrary.Repository;
using Newtonsoft.Json;

namespace LandRushLibrary.Units
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Monster : Unit, IAttackable
    {
        public MonsterID MonsterId { get; set; }
        public int SlainExp { get; set; }
        public MonsterGrade MonsterGrade { get; set; }
        public string PrefabName { get; set; }

        public override void AddDamage(int damage)
        {
            if (Alive == false)
                return;

            int addDamage = damage;

            if (addDamage < 0)
                addDamage = 0;

            CurrentHp -= addDamage;

            OnAttacked(new AttackedEventArgs(this));

            if(CurrentHp <= 0 && Alive == true)
            {
                Alive = false;
                OnDead(new DeadEventArgs(this));
            }
        }

        protected override void OnDead(DeadEventArgs e)
        {
            List<DropInfo> dropInfos = MonsterItemDropManager.Instance.DropItem(MonsterGrade);

            if( dropInfos.Count > 0)
            {
                OnItemDropped(new ItemDroppedEventArgs(dropInfos));
            }

            base.OnDead(e);
        }

        public event Predicate<Unit> CorrectTargetUnit;
        public void Attack(Unit attakedUnit, int weaponDamage = 0, bool guard = false)
        {
            if (CorrectTargetUnit != null && CorrectTargetUnit(attakedUnit))
                return;

            int damage = AttackPower;

            CalculatedRandomDamageEventArgs args = new CalculatedRandomDamageEventArgs(damage);
            OnCalculatedRandomDamage(args);

            damage = args.AttackPower;
            
            int armor = attakedUnit.Armor;

            if (guard && (attakedUnit is Player player))
            {
                armor += player.ShieldArmor;
            }

            damage -= armor;
            if (damage < 0)
                damage = 0;

            attakedUnit.AddDamage(damage);

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
            clone.MonsterGrade = MonsterGrade;
            clone.Alive = true;
 
            return clone;
        }

        #region ItemDropped event things for C# 3.0
        public event EventHandler<ItemDroppedEventArgs> ItemDropped;

        protected virtual void OnItemDropped(ItemDroppedEventArgs e)
        {
            if (ItemDropped != null)
                ItemDropped(this, e);
        }

        private ItemDroppedEventArgs OnItemDropped(List<DropInfo> dropItems)
        {
            ItemDroppedEventArgs args = new ItemDroppedEventArgs(dropItems);
            OnItemDropped(args);

            return args;
        }

        private ItemDroppedEventArgs OnItemDroppedForOut()
        {
            ItemDroppedEventArgs args = new ItemDroppedEventArgs();
            OnItemDropped(args);

            return args;
        }


        #endregion
    }

    public class ItemDroppedEventArgs : EventArgs
    {
        public List<DropInfo> DropItems { get; set; }

        public ItemDroppedEventArgs()
        {
        }

        public ItemDroppedEventArgs(List<DropInfo> dropItems)
        {
            DropItems = dropItems;
        }
    }

}
