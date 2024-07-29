using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.Interfaces;
using Api.Totem.Application.Mappers;
using Api.Totem.Domain.Interfaces.Repositories;

namespace Api.Totem.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public IEnumerable<ProductToShowDTO> List()
		{
			return _productRepository.List().MapToProductToShowDTO();
		}

		public ProductToShowDTO Get(string id)
		{
			return _productRepository.Get(id).MapToProductToShowDTO();
		}

		public ProductToShowDTO Create(ProductToCreateDTO productToCreateDTO)
		{
			if(productToCreateDTO is null)
				throw new ArgumentNullException(nameof(productToCreateDTO));

			var product = productToCreateDTO.MapToProduct();

			product.Id = Guid.NewGuid().ToString();
			product.Available = true;

			return _productRepository.Create(product).MapToProductToShowDTO();
		}

		public ProductToShowDTO Update(string id, ProductToUpdateDTO productToUpdateDTO)
		{
			if (productToUpdateDTO is null)
				throw new ArgumentNullException(nameof(productToUpdateDTO));

			var product = _productRepository.Get(id);

			product.Name = productToUpdateDTO.Name;
			product.Description = productToUpdateDTO.Description;
			product.Price = productToUpdateDTO.Price;

			return _productRepository.Update(product).MapToProductToShowDTO();
		}

		public ProductToShowDTO UpdateAvailability(string id, ProductToUpdateAvailabilityDTO productToUpdateAvailabilityDTO)
		{
			if (productToUpdateAvailabilityDTO is null)
				throw new ArgumentNullException(nameof(productToUpdateAvailabilityDTO));

			var product = _productRepository.Get(id);

			product.Available = productToUpdateAvailabilityDTO.Available;

			return _productRepository.Update(product).MapToProductToShowDTO();
		}

		public void Delete(string id)
		{
			_productRepository.Delete(id);
		}
	}
}
