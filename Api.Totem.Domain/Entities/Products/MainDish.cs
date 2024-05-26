using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities.Products
{
	public class MainDish : Product
	{
        public static List<SideDishSet> SideDishesAmountAllowed => new List<SideDishSet>
		{
			new SideDishSet(amount: 3, SideDishType.Normal),
			new SideDishSet(amount: 1, SideDishType.Special),
		};
        public decimal SoldSeparatelyPrice { get; set; }

        public MainDish() : base(ProductType.MainDish) { }
    }
}
