using Api.Totem.Application.DTOs.Categories;

namespace Api.Totem.Application.Interfaces
{
	public interface ICategoryService
	{
		IEnumerable<CategoryToShowDTO> List();
		CategoryToShowDTO Get(string id);
		CategoryToShowDTO Create(CategoryToCreateDTO categoryToCreateDTO);
		CategoryToShowDTO Update(string id, CategoryToUpdateDTO categoryToUpdateDTO);
		void Delete(string id);
		CategoryToShowDTO AddProducts(string id, CategoryProductsToAddDTO categoryProductsToAddDTO);
		CategoryToShowDTO RemoveProducts(string id, CategoryProductsToRemoveDTO categoryProductsToRemoveDTO);
	}
}
