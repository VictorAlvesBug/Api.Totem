using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs
{
	public class CategoryToUpdateDTO : CategoryToSaveDTO
	{
		public CategoryDTO ToCategoryDto(string id)
		{
			var category = new CategoryDTO
			{
				Id = id,
				CategoryType = CategoryType,
				Name = Name,
				ComplementType = ComplementType
			};

			FillComplementFields(category);

			return category;
		}
	}
}
