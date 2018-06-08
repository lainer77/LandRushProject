using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Items;
using LandRushLibrary.Utilities;
using Newtonsoft.Json;

namespace LandRushLibrary.Units
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Player : Unit, IAttackable
    {
        public int Level { get; set; }
        public int CurrentExp { get; set; }
        public int MaxExp { get; set; }

        [JsonIgnore]
        public bool IsCombatMode { get; set; }
        [JsonIgnore]
        public int ShieldArmor { get; set; }
        [JsonIgnore]
        public EquipmentItem LeftItem { get; set; }
        [JsonIgnore]
        public EquipmentItem RightItem { get; set; }

        #region singleton
        private static Player _instance;

        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = PlayerSerializer.Instance.DeSerialize();
                    _instance.Revivor();
                }
                return _instance;
            }
        }

        private Player()
        {
            //Player jsonPlayer = PlayerSerializer.Instance.DeSerialize();
            //Name = jsonPlayer.Name;
            //Level = jsonPlayer.Level;
            //AttackPower = jsonPlayer.AttackPower;
            //Armor = jsonPlayer.Armor;
            //MaxHp = jsonPlayer.MaxHp;
            //CurrentHp = MaxHp;
            //Speed = jsonPlayer.Speed;
            //Alive = true;

            //Name = "Player";
            //Level = 1;
            //AttackPower = 10;
            //Armor = 5;
            //MaxHp = 50;
            //CurrentHp = 50;
            //MaxExp = 200;
            //CurrentExp = 0;
            //Alive = true;
        }
        #endregion

        public void Revivor()
        {
            CurrentHp = MaxHp;
            Alive = true;
            IsCombatMode = false;
        }

        public void ChangeEquipment(EquipmentItem leftItem, EquipmentItem rightItem)
        {
            LeftItem = leftItem;
            RightItem = rightItem;

            OnPlayerEquipmentChanged(new PlayerEquipmentChangedEventArgs(rightItem, leftItem));
        }

        public void ChangecombatMode(bool combatMode)
        {
            IsCombatMode = combatMode;

            if(combatMode == false)
            {
                LeftItem = null;
                RightItem = null;
            }

            OncombatModeChanged(new CombatModeChangedEventArgs(combatMode));
        }

        public EquipmentItem GetRightItem()
        {
            return RightItem;
        }

        public EquipmentItem GetLeftItem()
        {
            return LeftItem;
        }

        public override void AddDamage(int damage)
        {
            if (Alive == false)
                return;

            CurrentHp -= damage;

            OnAttacked(new AttackedEventArgs(this));

            if (CurrentHp <= 0 && Alive == true)
            {
                Alive = false;
                OnDead(new DeadEventArgs(this));
            }
        }
        public event Predicate<Unit> CorrectTargetUnit;
        public void Attack(Unit attakedUnit, int weaponDamage = 0, bool guard = false)
        {
            if (attakedUnit.Alive == false)
                return;

            if (CorrectTargetUnit != null && CorrectTargetUnit(attakedUnit))
                return;
            int damage = AttackPower + weaponDamage;

            CalculatedRandomDamageEventArgs args = new CalculatedRandomDamageEventArgs(damage);
            OnCalculatedRandomDamage(args);

            damage = args.AttackPower;

            int armor = attakedUnit.Armor;
            
            damage -= armor;
            if (damage < 0)
                damage = 0;

            attakedUnit.AddDamage(damage);

            if (attakedUnit.CurrentHp <= 0)
                AddExperience(((Monster)attakedUnit).SlainExp);
        }

        private void AddExperience(int exp)
        {
            CurrentExp += exp;

            if ( CurrentExp >= MaxExp )
            {
                Level++;
                CurrentExp -= MaxExp;
                MaxExp = LevelManager.Instance.GetNextExp(Level, MaxExp);

                LevelManager.Instance.AddStat(this);
                CurrentHp = MaxHp;

                OnLevelUp(new LevelUpEventArgs(Level));
            }
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

            public PlayerEquipmentChangedEventArgs(EquipmentItem rightItem, EquipmentItem leftItem)
            {
                LeftItem = leftItem;
                RightItem = rightItem;
            }
        }
        #endregion

        #region combatModeChanged event things for C# 3.0

        public event EventHandler<CombatModeChangedEventArgs> combatModeChanged;



        protected virtual void OncombatModeChanged(CombatModeChangedEventArgs e)

        {

            if (combatModeChanged != null)

                combatModeChanged(this, e);

        }



        private CombatModeChangedEventArgs OncombatModeChanged(bool combatMode)

        {

            CombatModeChangedEventArgs args = new CombatModeChangedEventArgs(combatMode);

            OncombatModeChanged(args);



            return args;

        }



        private CombatModeChangedEventArgs OncombatModeChangedForOut()

        {

            CombatModeChangedEventArgs args = new CombatModeChangedEventArgs();

            OncombatModeChanged(args);



            return args;

        }



        public class CombatModeChangedEventArgs : EventArgs

        {

            public bool combatMode { get; set; }



            public CombatModeChangedEventArgs()

            {

            }



            public CombatModeChangedEventArgs(bool combatMode)

            {

                combatMode = combatMode;

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






