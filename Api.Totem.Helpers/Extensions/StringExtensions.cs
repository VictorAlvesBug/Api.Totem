using System.Text;
using System.Text.RegularExpressions;

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
		public static string ToSnakeCase(this string pascalCaseValue)
		{
			if (string.IsNullOrEmpty(pascalCaseValue))
				return pascalCaseValue;

			var sbSnakeCaseValue = new StringBuilder();

			foreach (char caracter in pascalCaseValue)
			{
				if(caracter.GetCharType() == CharType.UpperCaseLetter)
				sbSnakeCaseValue.Append($"_{caracter}");
			}

			return sbSnakeCaseValue.ToString().ReplaceRegex("(^_)|(_$)", "");
		}

		public static CharType GetCharType(this char character)
		{
			if (Regex.IsMatch(character.ToString(), "^[A-Z]$"))
				return CharType.UpperCaseLetter;

			if (Regex.IsMatch(character.ToString(), "^[a-z]$"))
				return CharType.LowerCaseLetter;

			if (Regex.IsMatch(character.ToString(), "^_$"))
				return CharType.UnderScore;
			
			return CharType.Other;
		}

		public static string ReplaceRegex(this string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement);
		}
	}

	public enum CharType
	{
		UpperCaseLetter = 1,
		LowerCaseLetter = 2,
		UnderScore = 3,
		Other = 4
	}
}
