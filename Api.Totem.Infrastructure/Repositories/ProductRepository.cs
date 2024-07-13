using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Utils;

namespace Api.Totem.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public IEnumerable<Product> List()
		{
			return FileUtils.GetListFromFile<Product>();
		}

		public Product Get(string id)
		{
			var products = FileUtils.GetListFromFile<Product>();

			return products.FirstOrDefault(p => p.Id == id)
				?? throw new ArgumentException($"No product was found with {nameof(id)} = {id}.");
		}

		public Product Create(Product product)
		{
			var products = FileUtils.GetListFromFile<Product>();

			FileUtils.SaveListToFile(products.Append(product));

			return product;
		}

		public Product Update(Product product)
		{
			var products = FileUtils.GetListFromFile<Product>();

			if(!products.SafeAny(item => item.Id == product.Id))
				throw new ArgumentException($"No product was found with {nameof(product.Id)} = {product.Id}.");

			products = products.Where(item => item.Id != product.Id);

			FileUtils.SaveListToFile(products.Append(product));

			return product;
		}

		public void Delete(string id)
		{
			var products = FileUtils.GetListFromFile<Product>();

			if (!products.SafeAny(product => product.Id == id))
				throw new ArgumentException($"No product was found with {nameof(id)} = {id}.");

			products = products.Where(product => product.Id != id);

			FileUtils.SaveListToFile(products);
		}
	}
}
