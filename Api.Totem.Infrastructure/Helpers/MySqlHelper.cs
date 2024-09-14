using Api.Totem.Helpers.Extensions;
using Moq;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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

		private static string GetValueAsString<TEntity>(this TEntity entity, PropertyInfo property)
		{
			dynamic? dynamicValue = property.GetValue(entity);

			if (dynamicValue == null)
				return null;

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
				case char charValue: return $"{charValue}";
				case bool boolValue: return $"{(boolValue ? "TRUE" : "FALSE")}";
				case DateTime dateTimeValue: return $"{dateTimeValue:yyyy-MM-dd HH:mm:ss}";
				case TimeSpan timeSpanValue: return $"'{timeSpanValue}'";
				case Guid guidValue: return $"'{guidValue}'";

				case object objectValue: return $"'{objectValue.SerializeToJson()}'";

				default:
					return $"'{dynamicValue}'";
			}
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
