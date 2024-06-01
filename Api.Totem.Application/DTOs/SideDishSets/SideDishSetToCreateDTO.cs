using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs.SideDishSets
{
	public class SideDishSetToCreateDTO
	{
		public int Amount { get; set; }
		public string CategoryId { get; set; }

		public SideDishSet ToSideDishSet() => new SideDishSet
		{
			Amount = Amount,
			CategoryId = CategoryId
		};
	}
}
