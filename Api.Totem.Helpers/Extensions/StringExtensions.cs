namespace Api.Totem.Helpers.Extensions
{
	public static class StringExtensions
	{
		public static string ToCamelCase(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			string first = value[0].ToString();
			string rest = value[1..];
			return $"{first.ToUpper()}{rest}";
		}
	}
}
