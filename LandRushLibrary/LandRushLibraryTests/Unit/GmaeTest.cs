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

namespace LandRushLibrary.Unit.Tests
{
    [TestClass()]
    public class GameTest
    {
        [TestMethod()]
        public void Orc가_생성되고_공격력이_10이여야한다()
        {
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);
            Assert.AreEqual(10, orc.AttackPower);
        }
        [TestMethod()]
        public void 플레이어가_가져오는데_그_방어력이_5여야함()
        {
           Assert.AreEqual(5, Player.Instance.Armor);
        }

        [TestMethod()]
        public void 플레이어가_Orc의_공격을_받아_5의_데미지를_입어야한다()
        {
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);
            Assert.AreEqual(5,orc.AttackPower - Player.Instance.Armor);
        }

        //[TestMethod()]
        //public void 플레이어가_죽는지_테스트()
        //{
        //    Player player = new Player();
        //    Monster orc = new Monster(MonsterID.ORC);

        //    orc.AttackPowerCalulated += test;
        //    player.UnitDead += playerDead;
        //    player.BeAttacked += playerAttaked;



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