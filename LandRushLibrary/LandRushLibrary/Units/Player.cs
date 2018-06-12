using System;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Utilities;
using Newtonsoft.Json;

namespace LandRushLibrary.Units
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Player : Unit, IAttackable
    {
        #region singleton
        private static Player _instance;

        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = PlayerSerializer.Instance.DeSerialize();
                    _instance.Equipment = new PlayerEquipment(_instance._maxPairCount);
                    _instance.CurrentHp = _instance.MaxHp;

                }

                return _instance;
            }
        }

        private Player()
        {
        }
        #endregion

        #region Fields
        public int Level { get; set; }
        public int CurrentExp { get; set; }
        public int MaxExp { get; set; }
        [JsonIgnore]
        public PlayerEquipment Equipment { get; private set; }
        [JsonIgnore]
        private readonly int _maxPairCount = 2;
        [JsonIgnore]
        public bool IsCombatMode { get; set; }

        #endregion

        #region Methods

        public override void GotDamage(int damage)
        {
            if (Alive == false)
                return;

            CurrentHp -= damage;

            OnAttacked(new AttackedEventArgs(this));

            if (Alive == false)
            {
                OnDead(new DeadEventArgs(this));
            }
        }
        public event Predicate<Unit> CorrectTargetUnit;
        public void Attack(Unit attakedUnit, int weaponDamage = 0)
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

            attakedUnit.GotDamage(damage);

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

        #endregion


        #region Events

        #region MonsterKilled
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

        #region LevelUp
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

        #endregion


    }
}






