using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryDTO
	{
		public string Id { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Name { get; set; }
		public IEnumerable<string> ProductIds { get; set; }
		public IEnumerable<ProductDTO> Products { get; set; }
		public ComplementType ComplementType { get; set; }
		public IEnumerable<SideDishSetDTO>? SideDishSets { get; set; }
		public IEnumerable<string>? ComboItemCategoryIds { get; set; }
		public IEnumerable<CategoryDTO>? ComboItemCategories { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }
	}
}
