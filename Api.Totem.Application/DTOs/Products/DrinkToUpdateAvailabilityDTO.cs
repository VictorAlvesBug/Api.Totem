using Api.Totem.Domain.Entities.Products;
using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Application.DTOs
{
	public class DrinkToUpdateAvailabilityDTO
	{
		public bool Available { get; set; }

		public Drink ToDrink(string id) => new Drink
		{
			Id = id,
			Available = Available
		};
	}
}
