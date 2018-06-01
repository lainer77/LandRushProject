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
using LandRushLibrary.ConcreteUnit;
using LandRushLibrary.Repository;

namespace LandRushLibrary.Unit.Tests
{
    [TestClass()]
    public class GameTest
    {
        [TestMethod()]
        public void Orc가_생성되고_공격력이_10이여야한다()
        {
            Monster orc = new Monster(UnitId.ORC);
            Assert.AreEqual(10, orc.Status.AttackPower);
        }

        [TestMethod()]
        public void 플레이어가_생성되고_방어력이_5여야함()
        {
            Player player = new Player();
            Assert.AreEqual(5, player.Status.Armor);
        }

        [TestMethod()]
        public void 플레이어가_공격을_받아_7의_데미지를_입어야한다()
        {
            Player player = new Player();
            Monster orc = new Monster(UnitId.ORC);

            orc.AttackPowerCalulated += test;

            DamageDiscriminator.Attack(orc, player);

            Assert.AreEqual(player.Status.MaxHp - 7, player.Status.CurrentHp);
        }

        [TestMethod()]
        public void 플레이어가_죽는지_테스트()
        {
            Player player = new Player();
            Monster orc = new Monster(UnitId.ORC);

            orc.AttackPowerCalulated += test;
            player.UnitDead += playerDead;
            player.BeAttacked += playerAttaked;
            

            DamageDiscriminator.Attack(orc, player);
            DamageDiscriminator.Attack(orc, player);
            DamageDiscriminator.Attack(orc, player);
            DamageDiscriminator.Attack(orc, player);
            DamageDiscriminator.Attack(orc, player);

        }
        [TestMethod()]
        public void 플레이어가_orc를죽이고경험치를획득한다()
        {
            Monster orc = new Monster(UnitId.ORC);
            Monster orcLord = new Monster(UnitId.ORC_LORD);
            Player player = new Player();

            orc.UnitDead += monsterDead;
            orcLord.AttackPowerCalulated += test;
            
            DamageDiscriminator.Attack(orcLord, orc);
            Console.WriteLine(orc.Status.CurrentHp);
            DamageDiscriminator.Attack(orcLord, orc);
            Console.WriteLine(orc.Status.CurrentHp);
            Console.WriteLine(player.Status.CurrentExp);
        }
        

        public void monsterDead(Object sender, Unit<MonsterInfo>.UnitDeadEventArgs e)
        {
            Player player = new Player();
            player.GetExperience((Monster) sender);

        }
        public void test(Object sender, AttackPowerCalulatedEventArgs e)
        {
            e.AttackPower = e.AttackPower;
        }

        public void playerDead(Object sender, Unit<PlayerInfo>.UnitDeadEventArgs e)
        {
            Console.WriteLine("죽음");
        }

        public void playerAttaked(Object sender, Unit<PlayerInfo>.BeAttackedEventArgs e)
        {
            Console.WriteLine(e.Info.CurrentHp);

        }
    }
}