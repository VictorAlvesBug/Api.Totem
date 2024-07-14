using Api.Totem.Helpers.DataAnnotations.Attributes;

namespace Api.Totem.Helpers.Extensions
{
	public static class GenericExtensions
	{
		public static string GetErrorMessageFor<TObject>(this TObject _, string propertyName) where TObject : class
		{
			var defaultErrorMessage = $"The field {propertyName} was not provided correctly.";

			var property = typeof(TObject).GetProperty(propertyName);

			if (property == null)
				return defaultErrorMessage;

			var errorMessageAttribute = property
				.GetCustomAttributes(typeof(WhenRequiredErrorMessageAttribute), false)
				.FirstOrDefault() as WhenRequiredErrorMessageAttribute;

			if (errorMessageAttribute == null)
				return defaultErrorMessage;

			return errorMessageAttribute.ErrorMessage;
		}
	}
}
