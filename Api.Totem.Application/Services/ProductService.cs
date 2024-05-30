using Api.Totem.Domain.Entities.Products;
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

		public List<TProduct> List<TProduct>() where TProduct : Product
		{
			return _productRepository.List<TProduct>();
		}

		public TProduct Get<TProduct>(string id) where TProduct : Product
		{
			return _productRepository.Get<TProduct>(id);
		}

		public TProduct Create<TProduct>(TProduct product) where TProduct : Product
		{
			return _productRepository.Create(product);
		}

		public TProduct Update<TProduct>(TProduct product) where TProduct : Product
		{
			return _productRepository.Update(product);
		}

		public TProduct UpdateAvailability<TProduct>(TProduct product) where TProduct : Product
		{
			return _productRepository.UpdateAvailability(product);
		}

		public void Delete<TProduct>(string id) where TProduct : Product
		{
			_productRepository.Delete<TProduct>(id);
		}
	}
}
