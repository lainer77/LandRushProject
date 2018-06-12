
using LandRushLibrary.Interfaces;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    public class IngredientItem : GameItem
    {

        public override GameItem Clone()
        {
            IngredientItem clone = new IngredientItem();
            CloneCore(clone);

            return clone;
        }

    }
}
