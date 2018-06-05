using LandRushLibrary.Repository;
using System.Collections.Generic;

namespace LandRushLibrary.Upgrade
{
    public class UpgradeCost
    {
        public Dictionary<ItemID, int> RequireIngredients { get; private set; }
        public float Rate { get; private set; }

        public void AddIngredient(ItemID id, int amount)
        {
            RequireIngredients.Add(id, amount);
        }

        public void SetProbability(float rate)
        {
            Rate = rate;
        }
    }
}
