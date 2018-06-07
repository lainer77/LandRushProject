using LandRushLibrary.Combat;
using LandRushLibrary.Factory;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LandRushLibraryTests
{
    [TestClass()]
    public class GameTest2
    {
        [TestMethod()]
        public void 테스트()
        {
            Assert.AreEqual(0, 0);
        }

        [TestMethod()]
        public void 공격테스트()
        {
            Player player = Player.Instance;
            Monster orc = MonsterFactory.Instance.Create(MonsterID.ORC);


            player.CalculatedRandomDamage += OnCalcDamage;

            player.Attack(orc);

            Console.WriteLine(orc.CurrentHp);
        }

        public void OnCalcDamage(object sender, EventArgs e)
        {
            ((CalculatedRandomDamageEventArgs)e).AttackPower += 10;

        }
    }


}
