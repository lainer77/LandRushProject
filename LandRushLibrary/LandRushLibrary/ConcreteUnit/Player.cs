using System;
using LandRushLibrary.Unit;

namespace LandRushLibrary.ConcreteUnit
{
    public class Player : Unit<PlayerInfo>
    {
        private int _shieldArmor;

        public Player()
        {
            Status = UnitInfoRepository.Instance.GetPlayerInfo();
        }

        public override void Damaged(int damage)
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

        public int GetExperience(Monster monster)
        {
            return Status.CurrentExp += monster.Status.SlainExp;
        }
        
        public void LevelUp()
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
    }
}
