using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Repositories
{
	public interface IBaseRepository<TEntity> where TEntity : BaseEntity
	{
		IEnumerable<TEntity> List();
		TEntity Get(string id);
		TEntity Create(TEntity product);
		TEntity Update(TEntity product);
		void Delete(string id);
	}
}
