using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryToShowDTO
	{
		public string Id { get; set; }
		public string CategoryType { get; set; }
		public string Name { get; set; }
		public List<ProductToShowDTO> Products { get; set; }
		public string ComplementType { get; set; }
		public List<SideDishSetToShowDTO>? SideDishSets { get; set; }
		public List<CategoryToShowDTO>? ComboItemCategories { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }

		public CategoryToShowDTO(Category category)
		{
			Id = category.Id;
			CategoryType = category.CategoryType.ToString();
			Name = category.Name;
			Products = category.Products?.Select(product => new ProductToShowDTO(product)).ToList();
			ComplementType = category.ComplementType.ToString();
			SideDishSets = category.SideDishSets?.Select(sideDishSet => new SideDishSetToShowDTO(sideDishSet)).ToList();
			ComboItemCategories = category.ComboItemCategories?.Select(comboItemCategory => new CategoryToShowDTO(comboItemCategory)).ToList();
			ComboAdditionalPrice = category.ComboAdditionalPrice;

		}
	}
}
