using Api.Totem.Domain.Enumerators;
using Api.Totem.Helpers.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Api.Totem.Application.Validations
{
	public class EnumValidationAttribute : ValidationAttribute
	{
		private readonly Type _enumType;

		public EnumValidationAttribute(Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException($"Type '{enumType.Name}' provided must be an Enum.");
			}

			_enumType = enumType;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success;
			}

			if(!Enum.IsDefined(_enumType, value))
			{
				var strValidValues = 
					EnumExtensions.GetAllValues(_enumType, (name, id) => $"{id} - '{name}'")
					.JoinThis(", ");

				return new ValidationResult($"Value {value} is not valid for enum type '{_enumType.Name}'. Valid values: {strValidValues}.");
			}

			return ValidationResult.Success;
		}
	}
}
