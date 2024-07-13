using Api.Totem.Application.DTOs.Categories;

namespace Api.Totem.Application.DTOs.SideDishSets
{
	public class SideDishSetDTO
	{
		public int Amount { get; set; }
		public string CategoryId { get; set; }
		public CategoryDTO Category { get; set; }
	}
}
