using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Repositories
{
	public interface IBaseRepository<TEntity> where TEntity : BaseEntity
	{
		IEnumerable<TEntity> List(List<string> attributesToGet = null);
		bool TryGet(string id, out TEntity entity, List<string> attributesToGet = null);
		TEntity Get(string id, List<string> attributesToGet = null);
		TEntity Create(TEntity entity);
		TEntity Update(TEntity entity);
		void Delete(string id);
	}
}
