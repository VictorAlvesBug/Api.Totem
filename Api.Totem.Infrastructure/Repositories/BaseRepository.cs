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

		public IEnumerable<TEntity> List(List<string> attributesToGet = null)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			var strAttributesToGet = ComputeAttributesToGet(attributesToGet);

			return dbConnection.Query<TEntity>($"SELECT {strAttributesToGet} FROM {_tableName}");
		}

		public bool TryGet(string id, out TEntity entity, List<string> attributesToGet = null)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			var strAttributesToGet = ComputeAttributesToGet(attributesToGet);

			entity = dbConnection.QueryFirstOrDefault<TEntity>($"SELECT {strAttributesToGet} FROM {_tableName} WHERE `{nameof(id)}` = @id");

			return entity != null;
		}

		public TEntity Get(string id, List<string> attributesToGet = null)
		{
			var exists = TryGet(id, out TEntity entity, attributesToGet);

			if(exists)
				return entity;

			throw new ArgumentException($"No {_tableName} was found with {nameof(BaseEntity.Id).ToSnakeCase()} = {id}.");
		}

		public TEntity Create(TEntity entity)
		{
			throw new NotImplementedException();

			/*var entities = DatabaseFileHelper.GetListFromFile<TEntity>();

			DatabaseFileHelper.SaveListToFile(entities.Append(entity));

			return entity;*/
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

		private string ComputeAttributesToGet(List<string> attributes)
		{
			if(attributes == null)
				return "*";

			return attributes.Select(attribute => $"`{attribute.ToSnakeCase()}`").JoinThis(",");
		}
	}
}
