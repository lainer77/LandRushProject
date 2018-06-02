
namespace LandRushLibrary.Items
{
    public class IngredientItem : GameItem
    {
        public override GameItem Clone()
        {
            IngredientItem clone = new IngredientItem();
            SetBasicCloneItem(clone);

            return clone;
        }
    }
}
