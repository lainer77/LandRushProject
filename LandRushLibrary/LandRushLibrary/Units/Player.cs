using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Enums;
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
            Name = "Player";
            AttackPower = 10;
            Armor = 5;
            MaxHp = 50;
            CurrentHp = 50;
        }
        #endregion

        public void ChangeEquipment(EquipmentItem leftItem, EquipmentItem rightItem)
        {
            LeftItem = leftItem;
            RightItem = rightItem;

            OnPlayerEquipmentChanged(new PlayerEquipmentChangedEventArgs(leftItem, rightItem));
        }

        public void ChangeCombatStatus(CombatStatus combatStatus)
        {
            OnCombatStatusChanged(new CombatStatusChangedEventArgs(combatStatus));
        }


        public override void AddDamage(int damage)
        {
            CurrentHp -= damage;

            if (CurrentHp <= 0)
            {
                OnDead(new DeadEventArgs(this));
            }
        }
        public event Predicate<Unit> CorrectTargetUnit;
        public void Attack(Unit attakedUnit, int weaponDamage = 0, bool guard = false)
        {
            if (CorrectTargetUnit != null && CorrectTargetUnit(attakedUnit))
                return;
            int damage = AttackPower + weaponDamage;

            int armor = attakedUnit.Armor;

            damage -= armor;
            if (damage < 0)
                damage = 0;

            attakedUnit.AddDamage(damage);
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

        #region CombatStatusChanged event things for C# 3.0

        public event EventHandler<CombatStatusChangedEventArgs> CombatStatusChanged;



        protected virtual void OnCombatStatusChanged(CombatStatusChangedEventArgs e)

        {

            if (CombatStatusChanged != null)

                CombatStatusChanged(this, e);

        }



        private CombatStatusChangedEventArgs OnCombatStatusChanged(CombatStatus combatStatus)

        {

            CombatStatusChangedEventArgs args = new CombatStatusChangedEventArgs(combatStatus);

            OnCombatStatusChanged(args);



            return args;

        }



        private CombatStatusChangedEventArgs OnCombatStatusChangedForOut()

        {

            CombatStatusChangedEventArgs args = new CombatStatusChangedEventArgs();

            OnCombatStatusChanged(args);



            return args;

        }



        public class CombatStatusChangedEventArgs : EventArgs

        {

            public CombatStatus CombatStatus { get; set; }



            public CombatStatusChangedEventArgs()

            {

            }



            public CombatStatusChangedEventArgs(CombatStatus combatStatus)

            {

                CombatStatus = combatStatus;

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






