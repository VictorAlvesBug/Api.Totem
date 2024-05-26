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

		public TProduct Create<TProduct>(TProduct product) where TProduct : Product
		{
			var products = GetListFromFile<TProduct>();

			product.Id = Guid.NewGuid().ToString();

			products.Add(product);

			SaveListToFile(products);

			return product;
		}

		private List<TProduct> GetListFromFile<TProduct>() where TProduct : Product
		{

			(string filePath, string fileName, string strProductType) = GetProductSettings<TProduct>();

			return JsonConvert.DeserializeObject<Response<TProduct>>(File.ReadAllText(filePath))?.Data
					?? throw new Exception($"File {fileName} could not be converted to {nameof(Response<TProduct>)} with {nameof(Response<TProduct>)} as {strProductType}.");
		}

		private void SaveListToFile<TProduct>(List<TProduct> list) where TProduct : Product
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
