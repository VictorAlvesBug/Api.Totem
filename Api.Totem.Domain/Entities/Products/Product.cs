using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities.Products
{
	public class Product
	{
		public string Id { get; set; }
		public ProductType ProductType { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public decimal Price { get; set; }
		public bool Available { get; set; }

        public Product(ProductType productType)
        {
			ProductType = productType;
		}
    }
}
