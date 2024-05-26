using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities.Products
{
	public class SideDish : Product
	{
        public SideDishType SideDishType { get; set; }
        public decimal SoldSeparatelyPrice { get; set; }

		public SideDish() : base(ProductType.SideDish) { }
	}
}
