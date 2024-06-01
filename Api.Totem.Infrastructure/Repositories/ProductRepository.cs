using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Infrastructure.Models;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;
using Api.Totem.Helpers.Extensions;
using Newtonsoft.Json;
using Api.Totem.Infrastructure.Utils;

namespace Api.Totem.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public List<Product> List()
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

			product.Id = Guid.NewGuid().ToString();

			products.Add(product);

			FileUtils.SaveListToFile(products);

			return product;
		}

		public Product Update(Product product)
		{
			var products = FileUtils.GetListFromFile<Product>();

			if (!products.Any(p => p.Id == product.Id))
				throw new ArgumentException($"No product was found with {nameof(product.Id)} = {product.Id}.");

			products = products.Select(p =>
			{
				if (p.Id == product.Id)
					p = product;

				return p;
			}).ToList();

			FileUtils.SaveListToFile(products);

			return product;
		}

		public Product UpdateAvailability(Product product)
		{
			var products = FileUtils.GetListFromFile<Product>();

			if (!products.Any(p => p.Id == product.Id))
				throw new ArgumentException($"No product was found with {nameof(product.Id)} = {product.Id}.");

			products = products.Select(p =>
			{
				if (p.Id == product.Id)
				{
					p.Available = product.Available;
					product = p;
				}

				return p;
			}).ToList();

			FileUtils.SaveListToFile(products);

			return product;
		}

		public void Delete(string id)
		{
			var products = FileUtils.GetListFromFile<Product>();

			if (!products.Any(p => p.Id == id))
				throw new ArgumentException($"No product was found with {nameof(id)} = {id}.");

			products = products.Where(p => p.Id != id).ToList();

			FileUtils.SaveListToFile(products);
		}
	}
}
