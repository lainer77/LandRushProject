using System;
using LandRushLibrary.ConcreteUnit;
using LandRushLibrary.Unit;

namespace LandRushLibrary.Combat
{
    public class DamageDiscriminator
    {
        private DamageDiscriminator()
        {

        }

        public static void Attack<T>(IAttackable attacker, Unit<T> beHitUnit, bool guard = false) where T : UnitInfo
        {
            int damage = attacker.GetAttackPower();
            int armor = beHitUnit.Status.Armor;

            if (guard && ( beHitUnit is Player) )
            {
                Player player = beHitUnit as Player;
                armor += player.GetShieldArmor();
            }

            damage = damage - armor;
            if (damage < 0)
                damage = 0;

            beHitUnit.Damaged( damage );
        }
    }
}
