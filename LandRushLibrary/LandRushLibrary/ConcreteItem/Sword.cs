
using System;
using LandRushLibrary.Combat;
using LandRushLibrary.ConcreteUnit;
using LandRushLibrary.Item;
using LandRushLibrary.Unit;

namespace LandRushLibrary.ConcreteItem
{
    public class Sword : EquipmentItem<SwordInfo>, IAttackable
    {
        private PlayerInfo _playerInfo;

        public Sword(int itemId, PlayerInfo playerInfo)
        {
            Info = (SwordInfo)ItemInfoRepository.Instance.GetItemInfo(itemId);
            _playerInfo = playerInfo;
        }

        public int GetAttackPower()
        {
            return 1;
        }

        #region Events

        public event EventHandler<AttackPowerCalulatedEventArgs> AttackPowerCalulated;

        protected virtual void OnAttackPowerCalulated(AttackPowerCalulatedEventArgs e)
        {
            if (AttackPowerCalulated != null)
                AttackPowerCalulated(this, e);

        }

        private AttackPowerCalulatedEventArgs OnAttackPowerCalulated(int attackPower)
        {
            AttackPowerCalulatedEventArgs args = new AttackPowerCalulatedEventArgs(attackPower);
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

    }
}
