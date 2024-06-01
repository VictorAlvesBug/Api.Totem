using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Repositories
{
	public interface IProductRepository
	{
		List<Product> List();
		Product Get(string id);
		Product Create(Product product);
		Product Update(Product product);
		Product UpdateAvailability(Product product);
		void Delete(string id);
	}
}
