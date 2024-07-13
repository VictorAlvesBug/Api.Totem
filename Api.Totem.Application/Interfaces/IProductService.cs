
using Api.Totem.Application.DTOs.Products;

namespace Api.Totem.Application.Interfaces
{
	public interface IProductService
	{
		IEnumerable<ProductToShowDTO> List();
		ProductToShowDTO Get(string id);
		ProductToShowDTO Create(ProductToCreateDTO productToCreateDTO);
		ProductToShowDTO Update(string id, ProductToUpdateDTO productToUpdateDTO);
		ProductToShowDTO UpdateAvailability(string id, ProductToUpdateAvailabilityDTO productToUpdateAvailabilityDTO);
		void Delete(string id);
	}
}
