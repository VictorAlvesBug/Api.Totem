using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;
using System.Xml.Linq;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryToShowDTO
	{
		public string Id { get; set; }
		public string CategoryType { get; set; }
		public string Name { get; set; }
		public List<Product> Products { get; set; }
		public string ComplementType { get; set; }
		public List<SideDishSetToShowDTO>? SideDishSets { get; set; }
		public List<Category>? ComboItemCategories { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }

		public CategoryToShowDTO(Category category)
		{
			Id = category.Id;
			CategoryType = category.CategoryType.ToString();
			Name = category.Name;
			Products = category.Products;
			ComplementType = category.ComplementType.ToString();
			SideDishSets = category.SideDishSets?.Select(sideDishSet => new SideDishSetToShowDTO(sideDishSet)).ToList();
			ComboItemCategories = category.ComboItemCategories;
			ComboAdditionalPrice = category.ComboAdditionalPrice;

		}
	}
}
