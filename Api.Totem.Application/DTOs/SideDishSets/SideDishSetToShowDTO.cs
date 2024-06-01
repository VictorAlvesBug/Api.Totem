using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs.SideDishSets
{
	public class SideDishSetToShowDTO
	{
		public int Amount { get; set; }
		public Category Category { get; set; }

		public SideDishSetToShowDTO(SideDishSet sideDishSet)
		{
			Amount = sideDishSet.Amount;
			Category = sideDishSet.Category;
		}
	}
}
