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


namespace LandRushLibrary.Unit.Tests
{
    //[TestClass()]
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
            Player.PlayerEquipmentChangedEventArgs args = e as Player.PlayerEquipmentChangedEventArgs;

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

            for (int i = 0; i < 4; i++)
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
                Console.WriteLine(Player.Instance.CurrentHp);
            }
            Assert.AreEqual(0, Player.Instance.CurrentHp);
        }

        public void OnPlayerDead(Object sender, EventArgs e)
        {
            Console.WriteLine("쥬금");
        }

        [TestMethod()]
        public void 재료_아이템_정보_테스트()
        {
            IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.STONE);

            Assert.AreEqual("Stone", stone.Name);
            Assert.AreEqual(ItemID.STONE, stone.ItemId);
            Assert.AreEqual(ItemType.Ingredient, stone.Type);

            IngredientItem wood = ItemFactory.Instance.Create<IngredientItem>(ItemID.WOOD);

            Assert.AreEqual("Wood", wood.Name);
            Assert.AreEqual(ItemID.WOOD, wood.ItemId);
            Assert.AreEqual(ItemType.Ingredient, wood.Type);

            IngredientItem iron = ItemFactory.Instance.Create<IngredientItem>(ItemID.IRON);

            Assert.AreEqual("Iron", iron.Name);
            Assert.AreEqual(ItemID.IRON, iron.ItemId);
            Assert.AreEqual(ItemType.Ingredient, iron.Type);
        }

        [TestMethod()]
        public void 인벤토리_이이템_추가_테스트()
        {
            IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.STONE);
            InventoryManager.Instance.AddInvenItem(stone.ItemId);
            InventoryManager.Instance.AddInvenItem(stone.ItemId);
            InventoryManager.Instance.AddInvenItem(stone.ItemId);

            Assert.AreEqual(1, InventoryManager.Instance.Items.Count);

            for (int i = 0; i < 10; i++)
            {
                InventoryManager.Instance.AddInvenItem(stone.ItemId);
            }

            Assert.AreEqual(2, InventoryManager.Instance.Items.Count);

            Console.WriteLine(InventoryManager.Instance.Items[0].Amount);
            Console.WriteLine(InventoryManager.Instance.Items[1].Amount);

            InventoryManager.Instance.RemoveItem(ItemID.STONE, 13);

            Assert.AreEqual(0, InventoryManager.Instance.Items.Count);

            InventoryManager.Instance.InventoryIsFull += Full;

            for (int i = 0; i < 121; i++)
            {
                InventoryManager.Instance.AddInvenItem(stone.ItemId);
            }


        }

        public void Full(Object sender, EventArgs e)
        {
            Console.WriteLine("꽉참");
        }

        [TestMethod()]
        public void 인벤토리_아이템_변경_테스트()
        {
            InventoryManager.Instance.ClearInventory();

            IngredientItem stone = ItemFactory.Instance.Create<IngredientItem>(ItemID.STONE);
            IngredientItem wood = ItemFactory.Instance.Create<IngredientItem>(ItemID.WOOD);

            InventoryManager.Instance.AddInvenItem(stone.ItemId); 
            InventoryManager.Instance.AddInvenItem(wood.ItemId);

            Assert.AreEqual("Stone", InventoryManager.Instance.Items[0].Item.Name);

            InventoryManager.Instance.ExchangeSlotItem(0, 1);

            Assert.AreEqual("Wood",InventoryManager.Instance.Items[0].Item.Name);
        }

 



    }
}
