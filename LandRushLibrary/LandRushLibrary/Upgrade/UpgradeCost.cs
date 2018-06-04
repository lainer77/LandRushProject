using LandRushLibrary.Repository;
using System.Collections.Generic;

namespace LandRushLibrary.Upgrade
{
    public class UpgradeCost
    {
        public Dictionary<ItemID, int> IngredientAmount { get; private set; }
        public float Probability { get; private set; }

        public void AddIngredient(ItemID id, int amount)
        {
            IngredientAmount.Add(id, amount);
        }

        public void SetProbability(float probability)
        {
            Probability = probability;
        }
    }
}
