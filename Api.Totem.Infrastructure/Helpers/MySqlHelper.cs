using Api.Totem.Helpers.Extensions;
using System.Reflection;

namespace Api.Totem.Infrastructure.Helpers
{
	public static class MySqlHelper
	{
		public static string ToExpression<TEntity>(this IEnumerable<string> attributes)
		{
			if (attributes == null)
				return "*";

			var validAttributes = typeof(TEntity).GetProperties().Select(prop => prop.Name).ToList();

			var notFoundAttributes = attributes.Except(validAttributes);

			if (notFoundAttributes.SafeAny())
				throw new ArgumentException($"The following attributes were not found in {typeof(TEntity).Name} entity: {notFoundAttributes.JoinThis(",")}.");

			return attributes.Select(attribute => $"`{attribute.ToSnakeCase()}`").JoinThis(",");
		}

		private static IEnumerable<PropertyInfo> GetFilteredProperties<TEntity>(this TEntity entity, List<string> except = null)
		{
			var properties = entity.GetType().GetProperties().AsEnumerable();

			if (except.SafeAny())
				properties = properties.Where(property => !except.Contains(property.Name));

			return properties;
		}

		public static string ConvertDynamicToString(dynamic? dynamicValue)
		{
			if (dynamicValue == null)
				return string.Empty;

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

		private static string GetValueAsString<TEntity>(this TEntity entity, PropertyInfo property)
		{
			return ConvertDynamicToString(property.GetValue(entity));
		}

		public static string GetAttributeNames<TEntity>(this TEntity entity, List<string> except = null)
		{
			var properties = entity.GetFilteredProperties(except);

			return properties.Select(property => $"`{property.Name}`").JoinThis(",");
		}

		public static string GetAttributeValues<TEntity>(this TEntity entity, List<string> except = null)
		{
			var properties = entity.GetFilteredProperties(except);

			return properties.Select(property => $"{entity.GetValueAsString(property)}").JoinThis(",");
		}
	}
}
