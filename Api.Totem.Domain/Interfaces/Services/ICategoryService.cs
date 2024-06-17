using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Services
{
	public interface ICategoryService
	{
		List<CategoryDTO> List();
		CategoryDTO Get(string id);
		CategoryDTO Create(CategoryDTO category);
		CategoryDTO Update(CategoryDTO category);
		void Delete(string id);
	}
}
