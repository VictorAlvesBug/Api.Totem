using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Repositories
{
	public interface IBaseRepository<TEntity> where TEntity : BaseEntity
	{
		IEnumerable<TEntity> List();
		bool TryGet(string id, out TEntity entity);
		TEntity Get(string id);
		TEntity Create(TEntity entity);
		TEntity Update(TEntity entity);
		void Delete(string id);
	}
}
