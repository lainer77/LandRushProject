using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Items;
using Newtonsoft.Json;

namespace LandRushLibrary.Units
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Player : Unit, IAttackable
    {
        [JsonProperty]
        public int Level { get; set; }
        [JsonProperty]
        public int CurrentExp { get; set; }
        [JsonProperty]
        public int MaxExp { get; set; }

        public int ShieldArmor { get; set; }
        public EquipmentItem LeftItem { get; set; }
        public EquipmentItem RightItem { get; set; }

        public void ChangeEquipment(EquipmentItem leftItem, EquipmentItem rightItem)
        {
            LeftItem = leftItem;
            RightItem = rightItem;

            OnPlayerEquipmentChanged(new PlayerEquipmentChangedEventArgs(leftItem, rightItem));
        }


        #region singleton
        private static Player _instance;

        public static Player Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Player();
                return _instance;
            }
        }

        private Player()
        {
            MaxHp = 1000;
        }
        #endregion


        public override void GetDamage(int damage)
        {
            CurrentHp -= damage;

            if (CurrentHp <= 0)
            {
                OnDead(new DeadEventArgs(this));
            }
        }
        public event Predicate<Unit> CorrectTargetUnit;
        public void Attack(Unit attakedUnit, bool guard = false, int weaponDamage = 0)
        {
            if (CorrectTargetUnit != null && CorrectTargetUnit(attakedUnit))
                return;
            int demage = AttackPower + weaponDamage;

            int armor = attakedUnit.Armor;

            demage -= armor;
            if (demage < 0)
                demage = 0;

            attakedUnit.GetDamage(demage);
        }

        private void AddExperience(int exp)
        {
            CurrentExp += exp;
        }


        #region MonsterKilled event things for C# 3.0
        public event EventHandler<MonsterKilledEventArgs> MonsterKilled;

        protected virtual void OnMonsterKilled(MonsterKilledEventArgs e)
        {
            if (MonsterKilled != null)
                MonsterKilled(this, e);
        }

        private MonsterKilledEventArgs OnMonsterKilled(Monster monster)
        {
            MonsterKilledEventArgs args = new MonsterKilledEventArgs(monster);
            OnMonsterKilled(args);

            return args;
        }

        private MonsterKilledEventArgs OnMonsterKilledForOut()
        {
            MonsterKilledEventArgs args = new MonsterKilledEventArgs();
            OnMonsterKilled(args);

            return args;
        }

        public class MonsterKilledEventArgs : EventArgs
        {
            public Monster Monster { get; set; }

            public MonsterKilledEventArgs()
            {
            }

            public MonsterKilledEventArgs(Monster monster)
            {
                Monster = monster;
            }
        }
        #endregion

        #region LevelUp event things for C# 3.0
        public event EventHandler<LevelUpEventArgs> LevelUp;

        protected virtual void OnLevelUp(LevelUpEventArgs e)
        {
            if (LevelUp != null)
                LevelUp(this, e);
        }

        private LevelUpEventArgs OnLevelUp(int newLevel)
        {
            LevelUpEventArgs args = new LevelUpEventArgs(newLevel);
            OnLevelUp(args);

            return args;
        }

        private LevelUpEventArgs OnLevelUpForOut()
        {
            LevelUpEventArgs args = new LevelUpEventArgs();
            OnLevelUp(args);

            return args;
        }

        public class LevelUpEventArgs : EventArgs
        {
            public int NewLevel { get; set; }

            public LevelUpEventArgs()
            {
            }

            public LevelUpEventArgs(int newLevel)
            {
                NewLevel = newLevel;
            }
        }
        #endregion

        #region PlayerEquipmentChanged event things for C# 3.0
        public event EventHandler<PlayerEquipmentChangedEventArgs> PlayerEquipmentChanged;

        protected virtual void OnPlayerEquipmentChanged(PlayerEquipmentChangedEventArgs e)
        {
            if (PlayerEquipmentChanged != null)
                PlayerEquipmentChanged(this, e);
        }

        private PlayerEquipmentChangedEventArgs OnPlayerEquipmentChanged(EquipmentItem leftItem, EquipmentItem rightItem)
        {
            PlayerEquipmentChangedEventArgs args = new PlayerEquipmentChangedEventArgs(leftItem, rightItem);
            OnPlayerEquipmentChanged(args);

            return args;
        }

        private PlayerEquipmentChangedEventArgs OnPlayerEquipmentChangedForOut()
        {
            PlayerEquipmentChangedEventArgs args = new PlayerEquipmentChangedEventArgs();
            OnPlayerEquipmentChanged(args);

            return args;
        }

        public class PlayerEquipmentChangedEventArgs : EventArgs
        {
            public EquipmentItem LeftItem { get; set; }
            public EquipmentItem RightItem { get; set; }

            public PlayerEquipmentChangedEventArgs()
            {
            }

            public PlayerEquipmentChangedEventArgs(EquipmentItem leftItem, EquipmentItem rightItem)
            {
                LeftItem = leftItem;
                RightItem = rightItem;
            }
        }
        #endregion

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


    }
}
