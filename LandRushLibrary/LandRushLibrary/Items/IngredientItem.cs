
using LandRushLibrary.Interfaces;
using Newtonsoft.Json;

namespace LandRushLibrary.Items
{
    public class IngredientItem : GameItem, ICountable
    {

        private int _amount;
        [JsonIgnore]
        public int Amount
        {
            get { return _amount; }
            set
            {
                if (value <= _maxAmount)
                    _amount = value;
            }
        }
        [JsonProperty] private int _maxAmount;

        public int MaxAmount
        {
            get { return _maxAmount; }
            protected set { _maxAmount = value; }
        }

        public override GameItem Clone()
        {
            IngredientItem clone = new IngredientItem();
            CloneCore(clone);

            clone.MaxAmount = MaxAmount;
            clone.Amount = 1;

            return clone;
        }

    }
}
