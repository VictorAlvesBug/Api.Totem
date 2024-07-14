using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Api.Totem.Application.Validations
{
	public class CollectionSizeValidationAttribute : ValidationAttribute
	{
		private readonly int _minSize;
		private readonly int _maxSize;

		public CollectionSizeValidationAttribute(int minSize)
		{
			_minSize = minSize;
			_maxSize = int.MaxValue;
		}

		public CollectionSizeValidationAttribute(int minSize, int maxSize)
		{
			_minSize = minSize;
			_maxSize = maxSize;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var collection = value as IEnumerable;
			if (collection != null 
				&& collection.Cast<object>().Count() >= _minSize
				&& collection.Cast<object>().Count() <= _maxSize)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult($"The collection must contain at least {_minSize} and at most {_maxSize} items.");
		}
	}
}
