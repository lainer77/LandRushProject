using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Unit;

namespace LandRushLibrary.ConcreteUnit
{
    /// <summary>
    /// 작업중 : 남민우
    /// </summary>
     
    public class Player : Unit<PlayerInfo>, IAttackable
    {
        private int _shieldArmor;

        public Player()
        {
            Status = UnitInfoRepository.Instance.PlayerInfo;
        }

        public override void GetDamaged(int damage)
        {
            Status.CurrentHp -= damage;

            if (Status.CurrentHp <= 0)
            {
                OnUnitDead(new UnitDeadEventArgs(Status));
            }

            OnBeAttacked(new BeAttackedEventArgs(Status));
        }

        public int GetShieldArmor()
        {
            return _shieldArmor;
        }

        public void SetShieldArmor(int armor)
        {
            _shieldArmor = armor;
        }

        private int AddExperience(Monster monster)
        {
            return Status.CurrentExp += monster.Status.SlainExp;
        }

        private void LevelUp()
        {
            if (Status.CurrentExp > Status.MaxExp)
            {
                Status.Level++;
                Status.AttackPower += Status.Level;
                Status.Armor += Status.Armor;
                Status.MaxHp += (Status.Level * 10);
                Status.CurrentHp = Status.MaxHp;
                Status.CurrentExp -= Status.MaxExp;
                Status.MaxExp += (Status.Level * 100);
            }
        }

        public int GetAttackPower(int attackType)
        {
            AttackPowerCalulatedEventArgs args = new AttackPowerCalulatedEventArgs(Status.AttackPower, attackType);

            OnAttackPowerCalulated(args);

            return args.AttackPower;
        }

        #region Events
        public event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;

        protected virtual void OnAttackPowerCalulated(AttackPowerCalulatedEventArgs e)
        {
            if (AttackPowerCalulated != null)
                AttackPowerCalulated(this, e);

        }

        private AttackPowerCalulatedEventArgs OnAttackPowerCalulated(int attackPower, int attackType)
        {
            AttackPowerCalulatedEventArgs args = new AttackPowerCalulatedEventArgs(attackPower, attackType);
            OnAttackPowerCalulated(args);

            return args;
        }

        private AttackPowerCalulatedEventArgs OnAttackPowerCalulatedForOut()
        {
            AttackPowerCalulatedEventArgs args = new AttackPowerCalulatedEventArgs();
            OnAttackPowerCalulated(args);

            return args;
        }
        #endregion

        #region MonsterKilled event things for C# 3.0
        public event EventHandler<MonsterKilledEventArgs> MonsterKilled;

        protected virtual void OnMonsterKilled(MonsterKilledEventArgs e)
        {
            if (MonsterKilled != null)
                MonsterKilled(this, e);
        }

        private MonsterKilledEventArgs OnMonsterKilled(MonsterInfo monster )
        {
            MonsterKilledEventArgs args = new MonsterKilledEventArgs(monster );
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
            public MonsterInfo Monster { get; set;} 

            public MonsterKilledEventArgs()
            {
            }

            public MonsterKilledEventArgs(MonsterInfo monster )
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

        private LevelUpEventArgs OnLevelUp(int newLevel )
        {
            LevelUpEventArgs args = new LevelUpEventArgs(newLevel );
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
            public int NewLevel { get; set;} 

            public LevelUpEventArgs()
            {
            }

            public LevelUpEventArgs(int newLevel )
            {
                NewLevel = newLevel; 
            }
        }
        #endregion

        public void Attack(Unit<UnitInfo> monster)
        {
            int damage = GetAttackPower(1);
            int armor = monster.Status.Armor;

            if (monster is Player)
            {
//                Player player = monster as Player;
//                armor += player.GetShieldArmor();
            }

            damage = damage - armor;
            if (damage < 0)
                damage = 0;

            monster.GetDamaged( damage );

            if (monster.Status.CurrentHp < 0)
            {
                OnMonsterKilled((MonsterInfo) monster.Status);
            }

            if (Status.CurrentExp > 1000)
            {
                OnLevelUp(Status.Level + 1);
            }
        }
    }
}
