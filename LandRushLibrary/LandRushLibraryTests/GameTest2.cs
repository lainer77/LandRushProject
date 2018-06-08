using LandRushLibrary.Combat;
using LandRushLibrary.Factory;
using LandRushLibrary.ItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static LandRushLibrary.Units.Player;

namespace LandRushLibraryTests
{
    //[TestClass()]
    public class GameTest2
    {
        /// 전투 종합 테스트
        /// 
        [TestMethod()]
        public void Player_장비_세팅()
        {
            Player player = Player.Instance;
            PlayerEquipmentManager equipmentManager = PlayerEquipmentManager.Instance;

            equipmentManager.SetEquipmentToSlot(1, ItemFactory.Instance.Create<Sword>(ItemID.OLD_SWORD));
            equipmentManager.SetEquipmentToSlot(2, ItemFactory.Instance.Create<Bow>(ItemID.OLD_BOW));
            equipmentManager.SetEquipmentToSlot(3, ItemFactory.Instance.Create<Shield>(ItemID.OLD_SHIELD));
            equipmentManager.SetEquipmentToSlot(4, ItemFactory.Instance.Create<Quiver>(ItemID.OLD_QUIVER));

            player.PlayerEquipmentChanged += OnChangedPlayerEquipment;

            equipmentManager.EquipCurrentPair();
            
        }

        public void OnChangedPlayerEquipment(object sencer, PlayerEquipmentChangedEventArgs e )
        {
            Assert.AreEqual(ItemID.OLD_SWORD, e.RightItem.ItemId);
            Assert.AreEqual(ItemID.OLD_SHIELD, e.LeftItem.ItemId);
        }

        [TestMethod()]
        public void 오크_한대_쳐보기()
        {
            Player player = Player.Instance;
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);

            player.Attack(orc, ((Sword)player.RightItem).AttackPower);

            Assert.AreEqual(13, orc.CurrentHp);

            player.CalculatedRandomDamage += OnCalcDamage;

            player.Attack(orc, ((Sword)player.RightItem).AttackPower);


            Assert.AreEqual(1, orc.CurrentHp);

            player.CalculatedRandomDamage -= OnCalcDamage;
        }

        public void OnCalcDamage(object sender, CalculatedRandomDamageEventArgs e)
        {
            ((CalculatedRandomDamageEventArgs)e).AttackPower -= 5;
        }

        [TestMethod()]
        public void 오크를_한번_죽여보자()
        {
            Player player = Player.Instance;
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);

            orc.Attacked += OnAttacked;
            orc.Dead += OnDead;

            player.Attack(orc, ((Sword)player.RightItem).AttackPower);
            player.Attack(orc, ((Sword)player.RightItem).AttackPower);

            player.MonsterKilled += OnMonsterKilled;

            Assert.AreEqual(20, player.CurrentExp);

            player.MonsterKilled -= OnMonsterKilled;
            orc.Attacked -= OnAttacked;
            orc.Dead -= OnDead;

        }

        public void OnDead(object sender, DeadEventArgs e)
        {
            Assert.AreEqual(false, e.Unit.Alive);
        }

        public void OnAttacked(object sender, AttackedEventArgs e)
        {
            Assert.AreEqual(true, e.AttackedUnit.Alive);
        }

        public void OnMonsterKilled(object sender, MonsterKilledEventArgs e)
        {
            Assert.AreEqual(false, e.Monster.Alive);
        }

        [TestMethod()]
        public void 오크를_10번더_죽이면서_레벨업을_테스트_해보자()
        {
            Player player = Player.Instance;
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);

            player.LevelUp += OnLevelUp;

            for (int i = 0; i < 10; i++)
            {
                player.Attack(orc, ((Sword)player.RightItem).AttackPower);
                player.Attack(orc, ((Sword)player.RightItem).AttackPower);

                orc = MonsterFactory.Instance.Create(MonsterID.ORC);
            }

            Assert.AreEqual(20, player.CurrentExp);
            Assert.AreEqual(400, player.MaxExp);
            Assert.AreEqual(12, player.AttackPower);
            Assert.AreEqual(7, player.Armor);
            Assert.AreEqual(250, player.MaxHp);
        }

        public void OnLevelUp(object sender, LevelUpEventArgs e)
        {
            Assert.AreEqual(2, e.NewLevel);
        }


    }


}
