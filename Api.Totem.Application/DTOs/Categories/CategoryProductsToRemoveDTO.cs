using Api.Totem.Application.Validations;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryProductsToRemoveDTO
	{
		[CollectionSizeValidation(1, ErrorMessage = "The list must have at least 1 item.")]
		public IEnumerable<string> ProductIds { get; set; }
	}
}
