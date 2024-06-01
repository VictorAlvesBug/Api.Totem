using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Application.DTOs
{
	public class CategoryToUpdateDTO
	{
		public CategoryType CategoryType { get; set; }
		public string Name { get; set; }
		public ComplementType ComplementType { get; set; }
		public List<SideDishSet>? SideDishSets { get; set; }
		public List<string>? ComboItemCategoryIds { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }

		public Category ToCategory(string id)
		{
			var category = new Category
			{
				Id = id,
				CategoryType = CategoryType,
				Name = Name,
				ComplementType = ComplementType
			};

			switch (category.ComplementType)
			{
				case ComplementType.SideDishes:
					SideDishSets = SideDishSets
						?? throw new ArgumentNullException($"Categories with {nameof(ComplementType)} '{category.ComplementType}' must have a {nameof(SideDishSets)} list.");
					break;
				case ComplementType.OptionalCombo:
					ComboAdditionalPrice = ComboAdditionalPrice
						?? throw new ArgumentNullException($"Categories with {nameof(ComplementType)} '{category.ComplementType}' must have a {nameof(ComboAdditionalPrice)} value.");
					break;
			}

			return category;
		}
	}
}
