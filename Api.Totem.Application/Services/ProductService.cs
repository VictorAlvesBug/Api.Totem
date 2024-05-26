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

		public TProduct Create<TProduct>(TProduct product) where TProduct : Product
		{
			return _productRepository.Create<TProduct>(product);
		}
	}
}
