using LandRushLibrary.Factory;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandRushLibraryTests
{
    [TestClass]
    public class PlayerEquipmentTest
    {
        [TestMethod]
        public void 장비_장착()
        {
            Player player = Player.Instance;

            Sword sword = ItemFactory.Instance.Create<Sword>(ItemID.OldSword);
            Shield shield = ItemFactory.Instance.Create<Shield>(ItemID.OldShield);
            Bow bow = ItemFactory.Instance.Create<Bow>(ItemID.OldBow);
            Arrow arrow = ItemFactory.Instance.Create<Arrow>(ItemID.Arrow);

            player.Equipment.EquipItem(EquipmentSlot.Right, sword);
            player.Equipment.EquipItem(EquipmentSlot.Left, shield);

            Assert.AreEqual(sword, player.Equipment.RightEquipment);
            Assert.AreEqual(shield, player.Equipment.LeftEquipment);

            player.Equipment.ChangeNextPair();

            player.Equipment.EquipItem(EquipmentSlot.Left, bow);
            player.Equipment.EquipItem(EquipmentSlot.Right, arrow);

            Assert.AreEqual(bow, player.Equipment.LeftEquipment);
            Assert.AreEqual(arrow, player.Equipment.RightEquipment);

            player.Equipment.ChangeNextPair();


            Assert.AreEqual(sword, player.Equipment.RightEquipment);
            Assert.AreEqual(shield, player.Equipment.LeftEquipment);

        }

    }
}
