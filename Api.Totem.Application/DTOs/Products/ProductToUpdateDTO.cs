using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs.Products
{
	public class ProductToUpdateDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
