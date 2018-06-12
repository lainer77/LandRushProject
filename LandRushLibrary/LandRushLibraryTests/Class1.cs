
using LandRushLibrary.Factory;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using LandRushLibrary.Upgrade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LandRushLibraryTests
{
    [TestClass()]
    public class Class1
    {
        //[TestMethod()]
        //public void 일단_재료템_채워보자()
        //{
        //    Inventory inven = Inventory.Instance;

        //    IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.Stone);
        //    IngredientItem wood = ItemFactory.Instance.Create<IngredientItem>(ItemID.Wood);
        //    IngredientItem iron = ItemFactory.Instance.Create<IngredientItem>(ItemID.Iron);

        //    for (int i = 0; i < 40; i++)
        //    {
        //        inven.AddInvenItem(stone.ItemId);
        //        inven.AddInvenItem(wood.ItemId);
        //        inven.AddInvenItem(iron.ItemId);
        //    }

        //    System.Console.WriteLine(inven.GetAmountForId(ItemID.Stone));
        //    System.Console.WriteLine(inven.GetAmountForId(ItemID.Wood));
        //    System.Console.WriteLine(inven.GetAmountForId(ItemID.Iron));


        //}

        //[TestMethod]
        //public void 강화를_해볼까()
        //{
        //    Sword oldSword = ItemFactory.Instance.Create<Sword>(ItemID.OldSword);

        //    System.Console.WriteLine(Upgrader.Instance.UpgradePossibility(oldSword));

        //    Upgrader.Instance.UpgradeTried += OnUpgradeTried;

        //    Assert.AreEqual(1, oldSword.Grade);

        //    Upgrader.Instance.Upgrade<Sword>(ref oldSword);

        //    Inventory inven = Inventory.Instance;

        //    Assert.AreEqual(ItemID.SHORT_SWORD, oldSword.ItemId);
        //    Assert.AreEqual(2, oldSword.Grade);
        //    Assert.AreEqual(38, inven.GetAmountForId(ItemID.Stone));
        //    Assert.AreEqual(38, inven.GetAmountForId(ItemID.Wood));

        //}

        //public void OnUpgradeTried(object sender, UpgradeTriedEventArgs e)
        //{
        //    Assert.AreEqual(true, e.Success);
        //}
    }
}
