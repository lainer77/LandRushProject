
using LandRushLibrary;
using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LandRushLibraryTests
{
    [TestClass]
    public class CombatTest
    {
        private Sword oldSword = ItemFactory.Instance.Create<Sword>(ItemID.OldSword);

        [TestMethod]
        public void 오크_치기()
        {
            Player player = Player.Instance;

            player.Equipment.EquipItem(EquipmentSlot.Right, oldSword);

            player.Equipment.EquipItem(EquipmentSlot.Left, ItemFactory.Instance.Create<Shield>(ItemID.OldShield));

            Assert.AreEqual(ItemID.OldSword, player.Equipment.RightEquipment.ItemId);

            player.DamageCalculated += OnDamageCalculated;

            Assert.AreEqual(10, player.AttackPower);
            Assert.AreEqual(10, oldSword.AttackPower);

            Monster orc = MonsterFactory.Instance.Create(MonsterID.Orc);

            Assert.AreEqual(3, orc.Armor);

            player.Attack(orc);

            Assert.AreEqual(23, orc.CurrentHp);

            player.Attack(orc);
            player.Attack(orc);

            Assert.AreEqual(false, orc.Alive);

        }


        public void OnDamageCalculated(object sender, DamageCalculatedEventArgs e)
        {
            e.AttackPower += oldSword.AttackPower;
        }

        public void OnOrcDead(object sender, Unit.DeadEventArgs e)
        {
            Assert.AreEqual(-11, e.Unit.CurrentHp);
            Assert.AreEqual(20, Player.Instance.CurrentExp);
        }


        [TestMethod]
        public void 레벨업_테스트()
        {
            Player player = Player.Instance;

            player.LevelUp += OnLevelUp;
            player.MonsterKilled += OnMonsterKilled;

            Assert.AreEqual(1, player.Level);

            for (int i = 0; i < 19; i++)
            {
                Monster temp = MonsterFactory.Instance.Create(MonsterID.Orc);

                for (int j = 0; j < 3; j++)
                {
                    player.Attack(temp);
                    player.Attack(temp);
                    player.Attack(temp);
                }
            }

            Assert.AreEqual(2, player.Level);
            Assert.AreEqual(400, player.MaxExp);
        }

        public void OnLevelUp(object sender, Player.LevelUpEventArgs e)
        {
            Assert.AreEqual(2, e.NewLevel);
        }

        public void OnMonsterKilled(object sender, Player.MonsterKilledEventArgs e)
        {
            Assert.AreEqual(20, e.Monster.SlainExp);
        }
    }




}
