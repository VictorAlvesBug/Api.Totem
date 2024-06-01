using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Domain.Interfaces.Services;

namespace Api.Totem.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public List<Category> List()
		{
			return _categoryRepository.List();
		}

		public Category Get(string id)
		{
			return _categoryRepository.Get(id);
		}

		public Category Create(Category category)
		{
			return _categoryRepository.Create(category);
		}

		public Category Update(Category category)
		{
			return _categoryRepository.Update(category);
		}

		public void Delete(string id)
		{
			_categoryRepository.Delete(id);
		}
	}
}
