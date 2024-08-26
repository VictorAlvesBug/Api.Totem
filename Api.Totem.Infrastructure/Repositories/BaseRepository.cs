using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Helpers;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Api.Totem.Infrastructure.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly string _entityName;
		private readonly string _tableName;
		private readonly string _connectionString;

		public BaseRepository()
		{
			_entityName = typeof(TEntity).Name.ToCamelCase();
			_tableName = typeof(TEntity).Name.ToSnakeCase();
			_connectionString = GetConnectionString();
		}

		private string GetConnectionString()
		{
			string user = Environment.GetEnvironmentVariable("MY_SQL_USER")
				?? throw new Exception("MY_SQL_USER environment variable was not found.");
			string password = Environment.GetEnvironmentVariable("MY_SQL_PASSWORD")
				?? throw new Exception("MY_SQL_PASSWORD environment variable was not found.");
			return $"Server=localhost;Database=totem;User={user};Password={password};";
		}

		public IEnumerable<TEntity> List()
		{
			using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
			{
				return dbConnection.QueryAsync<TEntity>($"SELECT * FROM {_tableName}").GetAwaiter().GetResult();
			}
		}

		public bool TryGet(string id, out TEntity entity)
		{
			var entities = DatabaseFileHelper.GetListFromFile<TEntity>();

			entity = entities.FirstOrDefault(item => item.Id == id);

			return entity != null;
		}

		public TEntity Get(string id)
		{
			var exists = TryGet(id, out TEntity entity);

			if(exists)
				return entity;

			throw new ArgumentException($"No {_entityName} was found with {nameof(BaseEntity.Id)} = {id}.");
		}

		public TEntity Create(TEntity entity)
		{
			var entities = DatabaseFileHelper.GetListFromFile<TEntity>();

			DatabaseFileHelper.SaveListToFile(entities.Append(entity));

			return entity;
		}

		public TEntity Update(TEntity entity)
		{
			var entities = DatabaseFileHelper.GetListFromFile<TEntity>();

			if (!entities.SafeAny(item => item.Id == entity.Id))
				throw new ArgumentException($"No {_entityName} was found with {nameof(BaseEntity.Id)} = {entity.Id}.");

			entities = entities.Where(item => item.Id != entity.Id);

			DatabaseFileHelper.SaveListToFile(entities.Append(entity));

			return entity;
		}

		public void Delete(string id)
		{
			var entities = DatabaseFileHelper.GetListFromFile<TEntity>();

			if (!entities.SafeAny(item => item.Id == id))
				throw new ArgumentException($"No {_entityName} was found with {nameof(BaseEntity.Id)} = {id}.");

			entities = entities.Where(entity => entity.Id != id);

			DatabaseFileHelper.SaveListToFile(entities);
		}
	}
}
