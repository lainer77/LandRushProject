
using LandRushLibrary.Factory;
using LandRushLibrary.ItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using System;
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

            _maxGrade = 4;
            _random = new Random((int)DateTime.Now.Ticks);

        }

        private List <UpgradeCost> _upgradeCosts;
        private int _maxGrade;
        private Random _random;

        public UpgradeCost GetUpgradeConst(EquipmentItem equipment)
        {
            return _upgradeCosts[equipment.Grade];
        }

        public UpgradeCost GetPlayerStock(EquipmentItem equipment)
        {
            UpgradeCost cost = _upgradeCosts[equipment.Grade];

            UpgradeCost stock = new UpgradeCost();

            InventoryManager inventory = InventoryManager.Instance;

            foreach (var item in cost.RequireIngredients)
            {
                stock.AddIngredient(item.Key, inventory.GetAmountForId(item.Key));
            }

            return stock;
        }

        public bool UpgradePossibility(EquipmentItem equipment)
        {
            if (equipment.Grade == _maxGrade)
                return false;

            UpgradeCost cost = _upgradeCosts[equipment.Grade];
            bool poss = true;

            foreach (var item in cost.RequireIngredients)
            {
                if ( item.Value > InventoryManager.Instance.GetAmountForId(item.Key))
                {
                    poss = false;
                    break;
                }
            }

            return false;
        }

        public void Upgrade<T>(T equipment) where T : EquipmentItem
        {
            if (!UpgradePossibility(equipment))
                return;

            InventoryManager inven = InventoryManager.Instance;
            UpgradeCost cost = _upgradeCosts[equipment.Grade];

            foreach (var item in cost.RequireIngredients)
            {
                inven.RemoveItem(item.Key, item.Value);
            }

            ItemID nextGradeItem = ItemFactory.Instance.FindItemId<EquipmentItem>( x => ( x.Type == equipment.Type )&&( x.Grade == equipment.Grade++));


            int random = _random.Next(100);
            int probability = (int)(_upgradeCosts[equipment.Grade].Probability * 100);

            if (random > probability)
                return;

            Type type = equipment.GetType();
            equipment = ItemFactory.Instance.Create<T>(nextGradeItem);

        }

        #region UpgradeTried event things for C# 3.0
        public event EventHandler<UpgradeTriedEventArgs> UpgradeTried;

        protected virtual void OnUpgradeTried(UpgradeTriedEventArgs e)
        {
            if (UpgradeTried != null)
                UpgradeTried(this, e);
        }

        private UpgradeTriedEventArgs OnUpgradeTried(bool success)
        {
            UpgradeTriedEventArgs args = new UpgradeTriedEventArgs(success);
            OnUpgradeTried(args);

            return args;
        }

        private UpgradeTriedEventArgs OnUpgradeTriedForOut()
        {
            UpgradeTriedEventArgs args = new UpgradeTriedEventArgs();
            OnUpgradeTried(args);

            return args;
        }

        public class UpgradeTriedEventArgs : EventArgs
        {
            public bool Success { get; set; }

            public UpgradeTriedEventArgs()
            {
            }

            public UpgradeTriedEventArgs(bool success)
            {
                Success = success;
            }
        }
        #endregion

    }
}
