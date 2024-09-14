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
			var entityType = typeof(TEntity);
			_entityName = entityType.Name;
			_tableName = entityType.Name.ToSnakeCase();
			_connectionString = GetConnectionString();
		}

		private string GetConnectionString()
		{
			string MY_SQL_USER = Environment.GetEnvironmentVariable(nameof(MY_SQL_USER))
				?? throw new Exception($"{nameof(MY_SQL_USER)} environment variable was not found.");
			string MY_SQL_PASSWORD = Environment.GetEnvironmentVariable(nameof(MY_SQL_PASSWORD))
				?? throw new Exception($"{nameof(MY_SQL_PASSWORD)} environment variable was not found.");
			return $"Server=localhost;Database=totem;User={MY_SQL_USER};Password={MY_SQL_PASSWORD};";
		}

		public IEnumerable<TEntity> List(List<string> attributesToGet = null)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			var query = $@"
				SELECT 
					{attributesToGet.ToExpression<TEntity>()} 
				FROM 
					{_tableName}
				";

			return dbConnection.Query<TEntity>(query);
		}

		public bool TryGet(string id, out TEntity? entity, List<string> attributesToGet = null)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			var query = $@"
				SELECT 
					{attributesToGet.ToExpression<TEntity>()} 
				FROM 
					{_tableName}
				WHERE 
					`{nameof(id)}` = @{nameof(id)}
				";

			entity = dbConnection.QueryFirstOrDefault<TEntity>(
				query, 
				new
				{
					id
				});

			return entity != null;
		}

		public TEntity Get(string id, List<string> attributesToGet = null)
		{
			var exists = TryGet(id, out TEntity? entity, attributesToGet);

			if (exists)
				return entity;

			throw new ArgumentException($"No item was found in {_tableName} with {nameof(BaseEntity.Id).ToSnakeCase()} = {id}.");
		}

		public TEntity Create(TEntity entity)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			string attributeNames = entity.GetAttributeNames();
			string attributeValues = entity.GetAttributeValues();

			var query = $@"
				INSERT INTO {_tableName}
					({attributeNames})
				VALUES
					({attributeValues})
				";

			var rowsAffected = dbConnection.Execute(query);

			if (rowsAffected > 0)
				return Get(entity.Id);

			throw new ArgumentException($"Error while trying to insert item into {_tableName}.");
		}

		public TEntity Update(TEntity entity)
		{
			throw new NotImplementedException();

			/*var entities = DatabaseFileHelper.GetListFromFile<TEntity>();

			if (!entities.SafeAny(item => item.Id == entity.Id))
				throw new ArgumentException($"No {_tableName} was found with {nameof(BaseEntity.Id).ToSnakeCase()} = {entity.Id}.");

			entities = entities.Where(item => item.Id != entity.Id);

			DatabaseFileHelper.SaveListToFile(entities.Append(entity));

			return entity;*/
		}

		public void Delete(string id)
		{
			throw new NotImplementedException();

			/*var entities = DatabaseFileHelper.GetListFromFile<TEntity>();

			if (!entities.SafeAny(item => item.Id == id))
				throw new ArgumentException($"No {_entityName} was found with {nameof(BaseEntity.Id)} = {id}.");

			entities = entities.Where(entity => entity.Id != id);

			DatabaseFileHelper.SaveListToFile(entities);*/
		}
	}
}
