using Api.Totem.Domain.Entities.Products;
using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Application.DTOs.Products
{
	public class DrinkToCreateDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public decimal Price { get; set; }

		public Drink ToDrink() => new Drink
		{
			ProductType = ProductType.Drink,
			Name = Name,
			Description = Description,
			CategoryId = CategoryId,
			Price = Price,
			Available = true
		};
	}
}
