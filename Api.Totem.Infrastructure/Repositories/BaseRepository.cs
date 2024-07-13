using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Utils;

namespace Api.Totem.Infrastructure.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
	{
        private readonly string _entityName;

        public BaseRepository()
		{
			_entityName = typeof(TEntity).Name.ToCamelCase();
		}

		public IEnumerable<TEntity> List()
		{
			return FileUtils.GetListFromFile<TEntity>();
		}

		public TEntity Get(string id)
		{
			var entities = FileUtils.GetListFromFile<TEntity>();

			return entities.FirstOrDefault(item => item.Id == id)
				?? throw new ArgumentException($"No {_entityName} was found with {nameof(id)} = {id}.");
		}

		public TEntity Create(TEntity entity)
		{
			var entities = FileUtils.GetListFromFile<TEntity>();

			FileUtils.SaveListToFile(entities.Append(entity));

			return entity;
		}

		public TEntity Update(TEntity entity)
		{
			var entities = FileUtils.GetListFromFile<TEntity>();

			if (!entities.SafeAny(item => item.Id == entity.Id))
				throw new ArgumentException($"No {_entityName} was found with {nameof(entity.Id)} = {entity.Id}.");

			entities = entities.Where(item => item.Id != entity.Id);

			FileUtils.SaveListToFile(entities.Append(entity));

			return entity;
		}

		public void Delete(string id)
		{
			var entities = FileUtils.GetListFromFile<TEntity>();

			if (!entities.SafeAny(item => item.Id == id))
				throw new ArgumentException($"No {_entityName} was found with {nameof(id)} = {id}.");

			entities = entities.Where(entity => entity.Id != id);

			FileUtils.SaveListToFile(entities);
		}
	}
}
