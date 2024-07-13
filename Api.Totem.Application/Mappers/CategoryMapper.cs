using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Domain.Entities;
using Api.Totem.Helpers.Extensions;

namespace Api.Totem.Application.Mappers
{
	public static class CategoryMapper
	{
		public static CategoryDTO MapToCategoryDTO(this Category category)
		{
			return category.ConvertTo<CategoryDTO>();
		}

		public static IEnumerable<CategoryDTO> MapToCategoryDTO(this IEnumerable<Category> categories)
		{
			return categories.Select(category => MapToCategoryDTO(category));
		}

		public static CategoryToShowDTO MapToCategoryToShowDTO(this CategoryDTO categoryDTO)
		{
			var categoryToShowDTO = categoryDTO.ConvertTo<CategoryToShowDTO>();

			categoryToShowDTO.CategoryType = categoryDTO.CategoryType.ToString();
			categoryToShowDTO.Products = categoryDTO.Products.ConvertTo<ProductToShowDTO>().ToList();
			categoryToShowDTO.ComplementType = categoryDTO.ComplementType.ToString();
			categoryToShowDTO.SideDishSets = categoryDTO.SideDishSets?.ConvertTo<SideDishSetToShowDTO>().ToList();
			categoryToShowDTO.ComboItemCategories = categoryDTO.ComboItemCategories?.ConvertTo<CategoryToShowDTO>().ToList();

			return categoryToShowDTO;
		}

		public static IEnumerable<CategoryToShowDTO> MapToCategoryToShowDTO(this IEnumerable<CategoryDTO> categoriesDTO)
		{
			return categoriesDTO.Select(categoryDTO => MapToCategoryToShowDTO(categoryDTO));
		}
		public static CategoryToShowDTO MapToCategoryToShowDTO(this Category category)
		{
			return category.ConvertTo<CategoryToShowDTO>();
		}

		public static IEnumerable<CategoryToShowDTO> MapToCategoryToShowDTO(this IEnumerable<Category> categories)
		{
			return categories.Select(category => MapToCategoryToShowDTO(category));
		}

		public static Category MapToCategory(this CategoryDTO categoryDTO)
		{
			return categoryDTO.ConvertTo<Category>();
		}

		public static IEnumerable<Category> MapToCategory(this IEnumerable<CategoryDTO> categoriesDTO)
		{
			return categoriesDTO.Select(categoryDTO => MapToCategory(categoryDTO));
		}

		public static Category MapToCategory(this CategoryToCreateDTO categoryToCreateDTO)
		{
			return categoryToCreateDTO.ConvertTo<Category>();
		}

		public static IEnumerable<Category> MapToCategory(this IEnumerable<CategoryToCreateDTO> categoriesToCreateDTO)
		{
			return categoriesToCreateDTO.Select(categoryToCreateDTO => MapToCategory(categoryToCreateDTO));
		}

		public static Category MapToCategory(this CategoryToUpdateDTO categoryToUpdateDTO)
		{
			return categoryToUpdateDTO.ConvertTo<Category>();
		}

		public static IEnumerable<Category> MapToCategory(this IEnumerable<CategoryToUpdateDTO> categoriesToUpdateDTO)
		{
			return categoriesToUpdateDTO.Select(categoryToUpdateDTO => MapToCategory(categoryToUpdateDTO));
		}
	}
}
