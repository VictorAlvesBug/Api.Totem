using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.DTOs.SideDishSets;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryToShowDTO
	{
		public string Id { get; set; }
		public string CategoryType { get; set; }
		public string Name { get; set; }
		public IEnumerable<ProductToShowDTO> Products { get; set; }
		public string ComplementType { get; set; }
		public IEnumerable<SideDishSetToShowDTO>? SideDishSets { get; set; }
		public IEnumerable<CategoryToShowDTO>? ComboItemCategories { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }
	}
}
