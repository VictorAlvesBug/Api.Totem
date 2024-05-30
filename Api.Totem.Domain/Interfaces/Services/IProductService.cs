using Api.Totem.Domain.Entities.Products;

namespace Api.Totem.Domain.Interfaces.Services
{
	public interface IProductService
	{
		List<TProduct> List<TProduct>() where TProduct : Product;
		TProduct Get<TProduct>(string id) where TProduct : Product;
		TProduct Create<TProduct>(TProduct product) where TProduct : Product;
		TProduct Update<TProduct>(TProduct product) where TProduct : Product;
		TProduct UpdateAvailability<TProduct>(TProduct product) where TProduct : Product;
		void Delete<TProduct>(string id) where TProduct : Product;
	}
}
