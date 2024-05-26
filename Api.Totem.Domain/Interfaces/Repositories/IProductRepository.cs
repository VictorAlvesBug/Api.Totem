using Api.Totem.Domain.Entities.Products;

namespace Api.Totem.Domain.Interfaces.Repositories
{
	public interface IProductRepository
	{
		List<TProduct> List<TProduct>() where TProduct : Product;
		TProduct Create<TProduct>(TProduct product) where TProduct : Product;
	}
}
