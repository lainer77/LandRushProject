
using LandRushLibrary.Interfaces;

namespace LandRushLibrary.Items
{
    public class IngredientItem : GameItem, ICountable
    {

        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public override GameItem Clone()
        {
            IngredientItem clone = new IngredientItem();
            CloneCore(clone);

            return clone;
        }

    }
}
