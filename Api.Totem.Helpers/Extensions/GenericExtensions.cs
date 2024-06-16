using Api.Totem.Helpers.DataAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Totem.Helpers.Extensions
{
	public static class GenericExtensions
	{
		public static string GetErrorMessageFor<TObject>(this TObject _, string propertyName) where TObject : class
		{
			var defaultErrorMessage = $"The field {propertyName} was not provided correctly.";

			var property = typeof(TObject).GetProperty(propertyName);

			if(property == null) 
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
