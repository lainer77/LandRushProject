
using LandRushLibrary.ItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using System.Collections.Generic;

namespace LandRushLibrary.Upgrade
{
    public class UpgradeManager
    {
        private static UpgradeManager _instance;
        public static UpgradeManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UpgradeManager();

                return _instance;
            }
        }


        private UpgradeManager()
        {
            UpgradeCost cost = new UpgradeCost();
            cost.SetProbability(0.8f);
            cost.AddIngredient(ItemID.STONE, 2);
            cost.AddIngredient(ItemID.WOOD, 2);
            cost.AddIngredient(ItemID.IRON, 0);
            _upgradeCosts.Add(cost);

            cost = new UpgradeCost();
            cost.SetProbability(0.5f);
            cost.AddIngredient(ItemID.STONE, 5);
            cost.AddIngredient(ItemID.WOOD, 5);
            cost.AddIngredient(ItemID.IRON, 0);
            _upgradeCosts.Add(cost);

            cost = new UpgradeCost();
            cost.SetProbability(0.3f);
            cost.AddIngredient(ItemID.STONE, 10);
            cost.AddIngredient(ItemID.WOOD, 10);
            cost.AddIngredient(ItemID.IRON, 10);
            _upgradeCosts.Add(cost);

        }

        private List <UpgradeCost> _upgradeCosts;

        public UpgradeCost GetUpgradeConst(EquipmentItem equipment)
        {
            return _upgradeCosts[equipment.Grade];
        }

        public UpgradeCost GetPlayerStock(EquipmentItem equipment)
        {
            UpgradeCost cost = _upgradeCosts[equipment.Grade];

            UpgradeCost stock = new UpgradeCost();

            InventoryManager inventory = InventoryManager.Instance;

            foreach (var item in cost.IngredientAmount)
            {
                stock.AddIngredient(item.Key, inventory.GetAmountForId(item.Key));
            }

            return stock;

        }
    }
}
