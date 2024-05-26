using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities
{
	public class SideDishSet
	{
		public int Amount { get; set; }
		public SideDishType SideDishType { get; set; }
		public string Description { get; set; }

        public SideDishSet(int amount, SideDishType sideDishType)
        {
			Amount = amount;
			SideDishType = sideDishType;
			var dishOrDishes = amount == 1 ? "Dish" : "Dishes";
			Description = $"{amount} {sideDishType} Side {dishOrDishes}";
		}
    }
}
