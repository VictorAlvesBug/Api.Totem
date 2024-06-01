using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Domain.Interfaces.Services;

namespace Api.Totem.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public List<Product> List()
		{
			return _productRepository.List();
		}

		public Product Get(string id)
		{
			return _productRepository.Get(id);
		}

		public Product Create(Product product)
		{
			return _productRepository.Create(product);
		}

		public Product Update(Product product)
		{
			return _productRepository.Update(product);
		}

		public Product UpdateAvailability(Product product)
		{
			return _productRepository.UpdateAvailability(product);
		}

		public void Delete(string id)
		{
			_productRepository.Delete(id);
		}
	}
}
