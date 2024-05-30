using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Infrastructure.Models;
using Api.Totem.Domain.Entities.Products;
using Api.Totem.Domain.Enumerators;
using Api.Totem.Helpers.Extensions;
using Newtonsoft.Json;

namespace Api.Totem.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private static readonly string _dataFolderPath = @"C:\Users\victo\OneDrive\Desktop\Pessoal\Projetos\Api.Totem\Api.Totem.Data";

		public List<TProduct> List<TProduct>() where TProduct : Product
		{
			return GetListFromFile<TProduct>();
		}

		public TProduct Get<TProduct>(string id) where TProduct : Product
		{
			var products = GetListFromFile<TProduct>();

			return products.FirstOrDefault(p => p.Id == id)
				?? throw new ArgumentException($"No product was found with {nameof(id)} = {id}.");
		}

		public TProduct Create<TProduct>(TProduct product) where TProduct : Product
		{
			var products = GetListFromFile<TProduct>();

			product.Id = Guid.NewGuid().ToString();

			products.Add(product);

			SaveListToFile(products);

			return product;
		}

		public TProduct Update<TProduct>(TProduct product) where TProduct : Product
		{
			var products = GetListFromFile<TProduct>();

			if (!products.Any(p => p.Id == product.Id))
				throw new ArgumentException($"No product was found with {nameof(product.Id)} = {product.Id}.");

			products = products.Select(p =>
			{
				if (p.Id == product.Id)
					p = product;

				return p;
			}).ToList();

			SaveListToFile(products);

			return product;
		}

		public TProduct UpdateAvailability<TProduct>(TProduct product) where TProduct : Product
		{
			var products = GetListFromFile<TProduct>();

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

			SaveListToFile(products);

			return product;
		}

		public void Delete<TProduct>(string id) where TProduct : Product
		{
			var products = GetListFromFile<TProduct>();

			if (!products.Any(p => p.Id == id))
				throw new ArgumentException($"No product was found with {nameof(id)} = {id}.");

			products = products.Where(p => p.Id != id).ToList();

			SaveListToFile(products);
		}

		private List<TProduct> GetListFromFile<TProduct>() where TProduct : Product
		{

			(string filePath, string fileName, string strProductType) = GetProductSettings<TProduct>();

			return JsonConvert.DeserializeObject<Response<TProduct>>(File.ReadAllText(filePath))?.Data
					?? throw new ArgumentException($"File {fileName} could not be converted to {nameof(Response<TProduct>)} with {nameof(Response<TProduct>)} as {strProductType}.");
		}

		private static void SaveListToFile<TProduct>(List<TProduct> list) where TProduct : Product
		{

			(string filePath, string _, string _) = GetProductSettings<TProduct>();

			File.WriteAllText(filePath, JsonConvert.SerializeObject(new Response<TProduct>(list)));
		}

		private static (string filePath, string fileName, string strProductType)
			GetProductSettings<TProduct>() where TProduct : Product
		{
			string strProductType = typeof(TProduct).Name;
			string fileName = strProductType.ToCamelCase();

			string filePath = $@"{_dataFolderPath}\{fileName}.json";

			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"File {fileName} not found for {nameof(ProductType)} {strProductType}.");
			}

			return (filePath, fileName, strProductType);
		}
	}
}
