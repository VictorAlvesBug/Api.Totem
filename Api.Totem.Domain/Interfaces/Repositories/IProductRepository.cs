using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Repositories
{
	public interface IProductRepository
	{
		IEnumerable<Product> List();
		Product Get(string id);
		Product Create(Product product);
		Product Update(Product product);
		void Delete(string id);
	}
}
