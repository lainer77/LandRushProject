
using LandRushLibrary.Factory;
using LandRushLibrary.Interfaces;
using LandRushLibrary.Items;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LandRushLibraryTests
{
    [TestClass]
    public class ItemTotalTest
    {
        [TestMethod]
        public void EqupmentItem생성_테스트()
        {
            Sword sword = ItemFactory.Instance.Create<Sword>(ItemID.OldSword);
            Shield shiedl = ItemFactory.Instance.Create<Shield>(ItemID.OldShield);
            Bow bow = ItemFactory.Instance.Create<Bow>(ItemID.OldBow);
            Arrow arrow = ItemFactory.Instance.Create<Arrow>(ItemID.Arrow);

            Sword sword2 = ItemFactory.Instance.Create<Sword>(ItemID.SteelSword);

            Assert.AreEqual(10, sword.AttackPower);
            Assert.AreEqual(1, sword.Grade);
            Assert.AreEqual(10, shiedl.Armor);
            Assert.AreEqual(10, bow.AttackPower);

            Assert.AreEqual(50, arrow.MaxAmount);

            arrow.Amount = 49;

            Assert.AreEqual( 49, arrow.Amount );

            arrow.Amount++;

            Assert.AreEqual(50, arrow.Amount );

            arrow.Amount++;

            Assert.AreEqual(50, arrow.Amount );

            arrow.Amount -= 10;

            Assert.AreEqual(40, arrow.Amount);

            arrow.Amount += 20;

            Assert.AreEqual(40, arrow.Amount);

            Assert.AreEqual(4, sword2.Grade);
        }


        [TestMethod]
        public void 기타_아이템_생성_테스트()
        {
            Potion potion = ItemFactory.Instance.Create<Potion>(ItemID.HpPotion);
            IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.Stone);
            IngredientItem wood = ItemFactory.Instance.Create<IngredientItem>(ItemID.Wood);
            IngredientItem iron = ItemFactory.Instance.Create<IngredientItem>(ItemID.Iron);


            Assert.AreEqual(0.25f, potion.RecorveyPoint);

            Assert.AreEqual("Stone", stone.Name);
            Assert.AreEqual("Wood", wood.Name);
            Assert.AreEqual("Iron", iron.Name);
        }

        [TestMethod]
        public void 인벤토리에_아이템_추가하기_테스트()
        {
            Inventory inven = Inventory.Instance;

            IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.Stone);

            inven.AddGameItem(stone);

            Assert.AreEqual(stone, inven.Items[0,0]);

            for (int i = 0; i < 30; i++)
            {
                inven.AddGameItem(stone);
            }

            Assert.AreEqual(30, inven.Items[0,0]);


        }
    }
}
