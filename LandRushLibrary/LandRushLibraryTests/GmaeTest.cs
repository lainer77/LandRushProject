using Microsoft.VisualStudio.TestTools.UnitTesting;
using LandRushLibrary.Unit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using LandRushLibrary.Combat;
using LandRushLibrary.Repository;
using LandRushLibrary.Factory;
using LandRushLibrary.Units;
using LandRushLibrary.Items;
using LandRushLibrary.ItemManagers;
using static LandRushLibrary.Units.Player;

namespace LandRushLibrary.Unit.Tests
{
    [TestClass()]
    public class GameTest
    {
        [TestMethod()]
        public void ORC가_생생되고_이름이Orc이고_공격력이10이여야_한다()
        {
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);
            Assert.AreEqual("Orc", orc.Name);
            Assert.AreEqual(10, orc.AttackPower);
        }

        [TestMethod()]
        public void 장비들을_생성하고_EquipmentManager에_Set한다()
        {
            Sword oldSword = (Sword)ItemFactory.Instance.Create(ItemID.OLD_SWORD);
            Shield oldShield = (Shield)ItemFactory.Instance.Create(ItemID.OLD_SHIELD);
            Bow oldBow = (Bow)ItemFactory.Instance.Create(ItemID.OLD_BOW);

            PlayerEquipmentManager.Instance.SetEquipmentToSlot(1, oldSword);
            PlayerEquipmentManager.Instance.SetEquipmentToSlot(3, oldShield);
            PlayerEquipmentManager.Instance.SetEquipmentToSlot(2, oldBow);

            Player.Instance.PlayerEquipmentChanged += EquipmentPairTest;

            PlayerEquipmentManager.Instance.EquipCurrentPair();

        }

        public void EquipmentPairTest(Object sender, EventArgs e)
        {
            PlayerEquipmentChangedEventArgs args = e as PlayerEquipmentChangedEventArgs;

            Console.WriteLine(args.RightItem.Name);
            Console.WriteLine(args.LeftItem.Name);

            Assert.AreEqual("OldSword", args.RightItem.Name);
            Assert.AreEqual("OldShield", args.LeftItem.Name);

        }

        public void 플레이어_공격_테스트한다()
        {
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);
            Sword sword = (Sword)ItemFactory.Instance.Create(ItemID.OLD_SWORD);

            Player.Instance.Attack(orc, sword.AttackPower);

            Console.WriteLine(orc.CurrentHp);
            Assert.AreEqual(20, orc.CurrentHp);
        }

        [TestMethod()]
        public void 플레이어_레벨업_테스트()
        {
            List<Monster> orcs = new List<Monster>();
            Player player = Player.Instance;

            for(int i = 0; i < 4; i++)
            {
                orcs.Add(MonsterFactory.Instance.Create(MonsterID.ORC));
                orcs[i].Dead += OnDead;
            }

            foreach (var orc in orcs)
            {
                for (int i = 0; i < 4; i++)
                {
                    player.Attack(orc, ((Sword)player.GetRightItem()).AttackPower);
                }
                Console.WriteLine(player.CurrentExp);
            }

        }

        public void OnLevelUp(Object sender, EventArgs e)
        {
            Console.WriteLine(Player.Instance.CurrentExp);
        }

        public void OnDead(Object sender, EventArgs e)
        {
            Console.WriteLine("Dead orc");
        }

        [TestMethod()]
        public void 플레이어가_죽는지_테스트한다()
        {
            Player.Instance.Dead += OnPlayerDead;
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);
            for (int i = 0; i < 11; i++)
            {
                orc.Attack(Player.Instance, orc.AttackPower, false);
            }
            Assert.AreEqual(0, Player.Instance.CurrentHp);
        }
        [TestMethod]
        public void 플레이어가_생성된_orc를_죽이고_경험치를_획득한다()
        {

        }

        public void OnPlayerDead(Object sender, EventArgs e)
        {
            Console.WriteLine("쥬금");
        }
            //}
            //[TestMethod()]
            //public void 플레이어가_orc를죽이고경험치를획득한다()
            //{
            //    Monster orc = new Monster(MonsterID.ORC);
            //    Monster orcLord = new Monster(MonsterID.ORC_LORD);
            //    Player player = new Player();

            //    orc.UnitDead += monsterDead;
            //    orcLord.AttackPowerCalulated += test;
            //    Console.WriteLine(orc.Status.CurrentHp);
            //    Console.WriteLine(orc.Status.CurrentHp);
            //    Console.WriteLine(player.Status.CurrentExp);
            //}


            //public void monsterDead(Object sender, Unit<MonsterInfo>.UnitDeadEventArgs e)
            //{
            //    Player player = new Player();
            //    player.GetExperience((Monster) sender);

            //}
            //public void test(Object sender, AttackPowerCalulatedEventArgs e)
            //{
            //    e.AttackPower = e.AttackPower;
            //}

            //public void playerDead(Object sender, Unit<PlayerInfo>.UnitDeadEventArgs e)
            //{
            //    Console.WriteLine("죽음");
            //}

            //public void playerAttaked(Object sender, Unit<PlayerInfo>.BeAttackedEventArgs e)
            //{
            //    Console.WriteLine(e.Info.CurrentHp);

            //}
        }
}
