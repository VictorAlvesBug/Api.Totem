using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Entities.Products;
using Api.Totem.Domain.Enumerators;
using System.Xml.Linq;

namespace Api.Totem.Application.DTOs.Products
{
	public class DrinkToShowDTO
	{
		public string Id { get; set; }
		public string ProductType { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public decimal Price { get; set; }
		public bool Available { get; set; }

        public DrinkToShowDTO(Drink drink)
        {
			Id = drink.Id;
			ProductType = drink.ProductType.ToString();
			Name = drink.Name;
			Description = drink.Description;
			CategoryId = drink.CategoryId;
			Price = drink.Price;
			Available = drink.Available;

		}
    }
}
