using System;
using LandRushLibrary.Combat;
using LandRushLibrary.Repository;

namespace LandRushLibrary.ConcreteUnit
{
    public class Player : Unit, IAttackable
    {
        public int Level { get; set; }
        public int CurrentExp { get; set; }
        public int MaxExp { get; set; }

        public override void GetDamage(int damage)
        {

        }

        public int GetAttackPower()
        {
            throw new NotImplementedException();
        }

        public void Attack(Unit attakedUnit)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;

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

        public event EventHandler<DoAttackEventArgs> DoAttack;

        protected virtual void OnDoAttack(DoAttackEventArgs e)
        {
            if (DoAttack != null)
                DoAttack(this, e);
        }

        private DoAttackEventArgs OnDoAttack(int attackPower)
        {
            DoAttackEventArgs args = new DoAttackEventArgs(attackPower);
            OnDoAttack(args);

            return args;
        }


    }
}
