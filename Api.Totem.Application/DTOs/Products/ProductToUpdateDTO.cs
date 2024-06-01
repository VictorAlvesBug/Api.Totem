using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs
{
	public class ProductToUpdateDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		public Product ToProduct(string id) => new Product
		{
			Id = id,
			Name = Name,
			Description = Description,
			Price = Price,
			Available = true
		};
	}
}
