using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs.Products
{
	public class ProductToCreateDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		public Product ToProduct() => new Product
		{
			Name = Name,
			Description = Description,
			Price = Price,
			Available = true
		};
	}
}
