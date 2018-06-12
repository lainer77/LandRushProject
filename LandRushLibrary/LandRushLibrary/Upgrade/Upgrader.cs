
using LandRushLibrary.Factory;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using LandRushLibrary.Interfaces;

namespace LandRushLibrary.Upgrade
{
    public class Upgrader
    {
        private static Upgrader _instance;
        public static Upgrader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Upgrader();

                return _instance;
            }
        }


        private Upgrader()
        {
            _upgradeCosts = new List<UpgradeCost>();

            UpgradeCost cost = new UpgradeCost();
            cost.SetProbability(0.8f);
            cost.AddIngredient(ItemID.Stone, 2);
            cost.AddIngredient(ItemID.Wood, 2);
            cost.AddIngredient(ItemID.Iron, 0);
            _upgradeCosts.Add(cost);

            cost = new UpgradeCost();
            cost.SetProbability(0.5f);
            cost.AddIngredient(ItemID.Stone, 5);
            cost.AddIngredient(ItemID.Wood, 5);
            cost.AddIngredient(ItemID.Iron, 0);
            _upgradeCosts.Add(cost);

            cost = new UpgradeCost();
            cost.SetProbability(0.3f);
            cost.AddIngredient(ItemID.Stone, 10);
            cost.AddIngredient(ItemID.Wood, 10);
            cost.AddIngredient(ItemID.Iron, 10);
            _upgradeCosts.Add(cost);

            cost = new UpgradeCost();
            cost.SetProbability(0.0f);
            cost.AddIngredient(ItemID.Stone, 0);
            cost.AddIngredient(ItemID.Wood, 0);
            cost.AddIngredient(ItemID.Iron, 0);
            _upgradeCosts.Add(cost);

            _maxGrade = 4;
            _random = new Random((int)DateTime.Now.Ticks);

        }

        private List <UpgradeCost> _upgradeCosts;
        private int _maxGrade;
        private Random _random;

        public UpgradeCost GetUpgradeCost(IUpgradable upgradable)
        {
            return _upgradeCosts[upgradable.Grade - 1];
        }

        public UpgradeCost GetPlayerStock(IUpgradable upgradable)
        {

            UpgradeCost cost = _upgradeCosts[upgradable.Grade - 1];

            UpgradeCost stock = new UpgradeCost();

            Inventory inventory = Inventory.Instance;

            foreach (var item in cost.RequireIngredients)
            {
                stock.AddIngredient(item.Key, inventory.GetAmountForId(item.Key));
            }

            return stock;
        }

        public bool UpgradePossibility(IUpgradable upgradable)
        {
            if (upgradable.Grade == _maxGrade)
                return false;

            UpgradeCost cost = _upgradeCosts[upgradable.Grade - 1];
            bool poss = true;

            foreach (var item in cost.RequireIngredients)
            {
                if ( item.Value > Inventory.Instance.GetAmountForId(item.Key))
                {
                    poss = false;
                    break;
                }
            }

            return poss;
        }

        public void Upgrade<T>(ref T equipment) where T : EquipmentItem, IUpgradable
        {
            if (!UpgradePossibility(equipment))
            {
                OnUpgradeTried(new UpgradeTriedEventArgs(false, true));
                return;
            }

            Inventory inven = Inventory.Instance;
            UpgradeCost cost = _upgradeCosts[equipment.Grade - 1];

            foreach (var item in cost.RequireIngredients)
            {
                inven.RemoveItem(item.Key, item.Value);
            }

            int random = _random.Next(100);
            int rate = (int)(_upgradeCosts[equipment.Grade -1].Rate * 100);

            if (random > rate)
            {
                OnUpgradeTried(new UpgradeTriedEventArgs(false, false));
                return;
            }

            ItemType type = equipment.Type;
            int grade = equipment.Grade;

            List<T> nextGradeItems = ItemFactory.Instance.FindItemListId<T>( x => ( x.Type == type )&&( x.Grade == (grade + 1) ));

            equipment = nextGradeItems.FirstOrDefault();

            OnUpgradeTried(new UpgradeTriedEventArgs(true, false));

        }

        #region UpgradeTried event things for C# 3.0
        public event EventHandler<UpgradeTriedEventArgs> UpgradeTried;

        protected virtual void OnUpgradeTried(UpgradeTriedEventArgs e)
        {
            if (UpgradeTried != null)
                UpgradeTried(this, e);
        }

        private UpgradeTriedEventArgs OnUpgradeTried(bool success, bool notEnoughIngreients)
        {
            UpgradeTriedEventArgs args = new UpgradeTriedEventArgs(success, notEnoughIngreients);
            OnUpgradeTried(args);

            return args;
        }

        private UpgradeTriedEventArgs OnUpgradeTriedForOut()
        {
            UpgradeTriedEventArgs args = new UpgradeTriedEventArgs();
            OnUpgradeTried(args);

            return args;
        }


        #endregion

    }

    public class UpgradeTriedEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public bool NotEnoughIngredients { get; set; }


        public UpgradeTriedEventArgs()
        {

        }

        public UpgradeTriedEventArgs(bool success, bool notEnoughIngreients)
        {
            Success = success;
            NotEnoughIngredients = notEnoughIngreients;
        }
    }
}
