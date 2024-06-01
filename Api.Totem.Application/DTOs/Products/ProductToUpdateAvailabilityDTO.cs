using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs
{
	public class ProductToUpdateAvailabilityDTO
	{
		public bool Available { get; set; }

		public Product ToProduct(string id) => new Product
		{
			Id = id,
			Available = Available
		};
	}
}
