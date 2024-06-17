using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryToCreateDTO : CategoryToSaveDTO
	{

		public CategoryDTO ToCategoryDto()
		{
			var category = new CategoryDTO
			{
				CategoryType = CategoryType,
				Name = Name,
				ProductIds = new List<string>(),
				ComplementType = ComplementType
			};

			FillComplementFields(category);

			return category;
		}
	}
}
