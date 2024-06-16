using System.Linq.Expressions;
using System.Xml.Linq;

namespace Api.Totem.Helpers.Extensions
{
	public static class EnumExtensions
	{
		public static IList<string> GetAllValues(
			Type enumType, 
			Func<string, string, string>? buildTemplate = null) 
		{
			buildTemplate ??= (string name, string id) => $"{name}";

			if (!enumType.IsEnum)
			{
				throw new ArgumentException($"Type '{enumType.Name}' provided must be an Enum.");
			}

			return Enum.GetValues(enumType).Cast<Enum>()
				.Select(item =>
				{
					string name = item.ToString();
					string id = Convert.ToInt64(item).ToString();

					return buildTemplate(name, id);
				}
				).ToList();
				
		}
	}
}
