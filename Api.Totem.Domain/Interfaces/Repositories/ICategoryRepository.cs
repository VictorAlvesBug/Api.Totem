using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Repositories
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> List();
		Category Get(string id);
		Category Create(Category category);
		Category Update(Category category);
		void Delete(string id);
	}
}
