using Api.Totem.Domain.Entities;

namespace Api.Totem.Domain.Interfaces.Services
{
	public interface IProductService
	{
		List<Product> List();
		Product Get(string id);
		Product Create(Product product);
		Product Update(Product product);
		Product UpdateAvailability(Product product);
		void Delete(string id);
	}
}
