using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Helpers;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using static Dapper.SqlMapper;

namespace Api.Totem.Infrastructure.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
	{
		private readonly string _tableName;
		private readonly string _connectionString;

		public BaseRepository()
		{
			var entityType = typeof(TEntity);
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

		public IEnumerable<TEntity> List(Dictionary<string, dynamic> conditions = null, List<string> attributesToGet = null)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			string attributeNames =
				attributesToGet.SafeAny()
				? new TEntity().GetFilteredAttributeNames(attributesToGet)
				: "*";

			string conditionsExpression = conditions.GetConditionsExpression();

			var query = $@"
				SELECT 
					{attributeNames} 
				FROM 
					{_tableName}
				{(conditions.SafeAny() ? $"WHERE {conditionsExpression}" : "")}
				";

			return dbConnection.Query<TEntity>(query);
		}

		public bool TryGet(string id, out TEntity? entity, List<string> attributesToGet = null)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			var filterAttributes = new List<string>
			{
				nameof(BaseEntity.Id)
			};

			string attributeNames =
				attributesToGet.SafeAny()
				? new TEntity().GetFilteredAttributeNames(attributesToGet)
				: "*";

			string attributeConditions = new TEntity().GetFilteredAttributeConditions(attributesToCompare: filterAttributes);

			var query = $@"
				SELECT 
					{attributeNames} 
				FROM 
					{_tableName}
				WHERE 
					{attributeConditions}
				";

			entity = dbConnection.QueryFirstOrDefault<TEntity>(
				query, 
				new
				{
					Id = id
				});

			return entity != null;
		}

		public TEntity Get(string id, List<string> attributesToGet = null)
		{
			var exists = TryGet(id, out TEntity? entity, attributesToGet);

			if (exists)
				return entity;

			throw new ArgumentException($"No item was found in {_tableName} with {nameof(BaseEntity.Id)} = {id}.");
		}

		public TEntity Create(TEntity entity)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			string attributeNames = entity.GetAllAttributeNames();
			string attributeValues = entity.GetAllAttributeValues();

			var query = $@"
				INSERT INTO {_tableName}
					({attributeNames})
				VALUES
					({attributeValues})
				";

			var rowsAffected = dbConnection.Execute(query, entity);

			if (rowsAffected > 0)
				return Get(entity.Id);

			throw new ArgumentException($"Error while trying to insert item into {_tableName}.");
		}

		public TEntity Update(TEntity entity)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			var filterAttributes = new List<string>
			{
				nameof(BaseEntity.Id)
			};

			string attributeAssignments = entity.GetAllAttributeAssignments(exceptByAttributeNames: filterAttributes);
			string attributeConditions = entity.GetFilteredAttributeConditions(attributesToCompare: filterAttributes);

			var query = $@"
				UPDATE 
					{_tableName}
				SET
					{attributeAssignments}
				WHERE
					{attributeConditions}
				";

			var rowsAffected = dbConnection.Execute(query, entity);

			if (rowsAffected > 0)
				return Get(entity.Id);

			throw new ArgumentException($"Error while trying to update item of {_tableName}.");
		}

		public void Delete(string id)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			var filterAttributes = new List<string>
			{
				nameof(BaseEntity.Id)
			};

			string attributeConditions = new TEntity().GetFilteredAttributeConditions(attributesToCompare: filterAttributes);

			var query = $@"
				DELETE FROM
					{_tableName}
				WHERE
					{attributeConditions}
				";

			var rowsAffected = dbConnection.Execute(
				query,
				new
				{
					Id = id
				});

			if (rowsAffected > 0)
				return;

			throw new ArgumentException($"Error while trying to update item of {_tableName}.");
		}

		public void Delete(Dictionary<string, dynamic> conditions = null)
		{
			using IDbConnection dbConnection = new MySqlConnection(_connectionString);

			string conditionsExpression = conditions.GetConditionsExpression();

			var query = $@"
				DELETE FROM
					{_tableName}
				{(conditions.SafeAny() ? $"WHERE {conditionsExpression}" : "")}
				";

			var rowsAffected = dbConnection.Execute(query);

			if (rowsAffected > 0)
				return;

			throw new ArgumentException($"Error while trying to update item of {_tableName}.");
		}
	}
}
