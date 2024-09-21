using Api.Totem.Helpers.Extensions;
using System.Reflection;

namespace Api.Totem.Infrastructure.Helpers
{
	public static class MySqlHelper
	{

		public static string GetAllAttributeNames<TEntity>(this TEntity entity, List<string> exceptByAttributeNames = null) =>
			entity
				.GetAllPropertiesExceptBy(exceptByAttributeNames)
				.Select(property => $"`{property.Name.ToSnakeCase()}`")
				.JoinThis();

		public static string GetAllAttributeValues<TEntity>(this TEntity entity, List<string> exceptByAttributeNames = null) =>
			entity
				.GetAllPropertiesExceptBy(exceptByAttributeNames)
				.Select(property => $"{entity.GetValueAsString(property)}")
				.JoinThis();

		public static string GetAllAttributeAssignments<TEntity>(this TEntity entity, List<string> exceptByAttributeNames = null) =>
			entity
				.GetAllPropertiesExceptBy(exceptByAttributeNames)
				.Select(property => $"`{property.Name.ToSnakeCase()}` = @{property.Name}")
				.JoinThis();

		public static string GetFilteredAttributeConditions<TEntity>(this TEntity entity, List<string> attributesToCompare) =>
			entity
				.GetFilteredProperties(attributesToCompare)
				.Select(property => $"`{property.Name.ToSnakeCase()}` = @{property.Name}")
				.JoinThis(" AND ");

		public static string GetFilteredAttributeNames<TEntity>(this TEntity entity, List<string> attributeNamesToGet) =>
			entity
				.GetFilteredProperties(attributeNamesToGet)
				.Select(property => $"`{property.Name.ToSnakeCase()}`")
				.JoinThis();

		public static string GetConditionsExpression(this Dictionary<string, dynamic> conditions) =>
			conditions
				.Select(kvp => $"`{kvp.Key.ToSnakeCase()}` = {ConvertDynamicToString(kvp.Value)}")
				.JoinThis(" AND ");

		private static IEnumerable<PropertyInfo> GetAllPropertiesExceptBy<TEntity>(this TEntity entity, List<string> exceptByAttributeNames = null)
		{
			var properties = entity.GetType().GetProperties().AsEnumerable();

			if (exceptByAttributeNames.SafeAny())
			{
				ValidateAttributeNames<TEntity>(exceptByAttributeNames);
				return properties.Where(property => !exceptByAttributeNames.Contains(property.Name));
			}

			return properties;
		}

		private static IEnumerable<PropertyInfo> GetFilteredProperties<TEntity>(this TEntity entity, List<string> attributesToGet = null)
		{
			if (!attributesToGet.SafeAny())
				throw new ArgumentException("Provide at least one attribute name to get.");

			ValidateAttributeNames<TEntity>(attributesToGet);

			return entity.GetType()
				.GetProperties()
				.AsEnumerable()
				.Where(property => attributesToGet.Contains(property.Name));
		}

		private static void ValidateAttributeNames<TEntity>(this IEnumerable<string> attributes)
		{
			var validAttributes = typeof(TEntity).GetProperties().Select(prop => prop.Name).ToList();

			var notFoundAttributes = attributes.Except(validAttributes);

			if (notFoundAttributes.SafeAny())
				throw new ArgumentException($"The following attributes were not found in {typeof(TEntity).Name} entity: {notFoundAttributes.JoinThis()}.");
		}
		private static string GetValueAsString<TEntity>(this TEntity entity, PropertyInfo property)
		{
			return ConvertDynamicToString(property.GetValue(entity));
		}
		
		private static string ConvertDynamicToString(dynamic? dynamicValue)
		{
			if (dynamicValue == null)
				return "NULL";

			switch (dynamicValue)
			{
				case int intValue: return $"{intValue}";
				case long longValue: return $"{longValue}";
				case short shortValue: return $"{shortValue}";
				case byte byteValue: return $"{byteValue}";
				case decimal decimalValue: return $"{decimalValue.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
				case float floatValue: return $"{floatValue.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
				case double doubleValue: return $"{doubleValue.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
				case string stringValue: return $"'{stringValue}'";
				case char charValue: return $"'{charValue}'";
				case bool boolValue: return $"{(boolValue ? "TRUE" : "FALSE")}";
				case DateTime dateTimeValue: return $"'{dateTimeValue:yyyy-MM-dd HH:mm:ss.fff}'";
				case TimeSpan ts: return $"'{ts.Days} {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}'";
				case Guid guidValue: return $"'{guidValue}'";

				case object objectValue: return $"'{objectValue.SerializeToJson()}'";

				default:
					return $"'{dynamicValue}'";
			}
		}

	}
}
