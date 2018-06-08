
using LandRushLibrary.Factory;
using LandRushLibrary.ItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using LandRushLibrary.Upgrade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LandRushLibraryTests
{
    [TestClass()]
    public class Class1
    {
        [TestMethod()]
        public void 일단_재료템_채워보자()
        {
            InventoryManager inven = InventoryManager.Instance;

            IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.STONE);
            IngredientItem wood = ItemFactory.Instance.Create<IngredientItem>(ItemID.WOOD);
            IngredientItem iron = ItemFactory.Instance.Create<IngredientItem>(ItemID.IRON);

            for (int i = 0; i < 40; i++)
            {
                inven.AddInvenItem(stone);
                inven.AddInvenItem(wood);
                inven.AddInvenItem(iron);
            }

            System.Console.WriteLine(inven.GetAmountForId(ItemID.STONE));
            System.Console.WriteLine(inven.GetAmountForId(ItemID.WOOD));
            System.Console.WriteLine(inven.GetAmountForId(ItemID.IRON));


        }

        [TestMethod]
        public void 강화를_해볼까()
        {
            Sword oldSword = ItemFactory.Instance.Create<Sword>(ItemID.OLD_SWORD);

            System.Console.WriteLine(UpgradeManager.Instance.UpgradePossibility(oldSword));

            UpgradeManager.Instance.UpgradeTried += OnUpgradeTried;

            Assert.AreEqual(1, oldSword.Grade);

            UpgradeManager.Instance.Upgrade<Sword>(ref oldSword);

            InventoryManager inven = InventoryManager.Instance;

            Assert.AreEqual(ItemID.SHORT_SWORD, oldSword.ItemId);
            Assert.AreEqual(2, oldSword.Grade);
            Assert.AreEqual(38, inven.GetAmountForId(ItemID.STONE));
            Assert.AreEqual(38, inven.GetAmountForId(ItemID.WOOD));

        }

        public void OnUpgradeTried(object sender, UpgradeTriedEventArgs e)
        {
            Assert.AreEqual(true, e.Success);
        }
    }
}
