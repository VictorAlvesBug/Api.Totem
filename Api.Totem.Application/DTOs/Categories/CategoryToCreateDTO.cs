using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryToCreateDTO
	{
		public CategoryType CategoryType { get; set; }
		public string Name { get; set; }
		public ComplementType ComplementType { get; set; }
		public List<SideDishSetToCreateDTO>? SideDishSets { get; set; }
		public List<string>? ComboItemCategoryIds { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }

		public Category ToCategory()
		{
			var category = new Category
			{
				CategoryType = CategoryType,
				Name = Name,
				ProductIds = new List<string>(),
				ComplementType = ComplementType
			};

			switch (category.ComplementType)
			{
				case ComplementType.SideDishes:
					category.SideDishSets = SideDishSets?.Select(sideDishSet => sideDishSet.ToSideDishSet()).ToList()
						?? throw new ArgumentNullException($"Categories with {nameof(ComplementType)} '{category.ComplementType}' must have a {nameof(SideDishSets)} list.");
					break;
				case ComplementType.OptionalCombo:
					category.ComboItemCategoryIds = ComboItemCategoryIds
						?? throw new ArgumentNullException($"Categories with {nameof(ComplementType)} '{category.ComplementType}' must have a {nameof(ComboItemCategoryIds)} list.");
					category.ComboAdditionalPrice = ComboAdditionalPrice
						?? throw new ArgumentNullException($"Categories with {nameof(ComplementType)} '{category.ComplementType}' must have a {nameof(ComboAdditionalPrice)} value.");
					break;
			}

			return category;
		}
	}
}
