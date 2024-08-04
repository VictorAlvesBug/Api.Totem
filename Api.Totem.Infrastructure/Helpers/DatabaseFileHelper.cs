using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Models;
using Newtonsoft.Json;

namespace Api.Totem.Infrastructure.Helpers
{
	public static class DatabaseFileHelper
	{
		private static readonly string _dataFolderPath = @"C:\Users\victo\OneDrive\Desktop\Pessoal\Projetos\Api.Totem\Api.Totem.Data";

		public static IEnumerable<TData> GetListFromFile<TData>()
		{
			string dataTypeName = typeof(TData).Name;
			var filePath = GetFilePath(dataTypeName);

			return JsonConvert.DeserializeObject<Response<TData>>(File.ReadAllText(filePath))?.Data
					?? throw new ArgumentException($"File '{filePath}' could not be converted to '{nameof(Response<TData>)}' type.");
		}

		public static void SaveListToFile<TData>(IEnumerable<TData> list)
		{
			string dataTypeName = typeof(TData).Name;
			var filePath = GetFilePath(dataTypeName);

			File.WriteAllText(filePath, JsonConvert.SerializeObject(new Response<TData>(list)));
		}

		private static string GetFilePath(string dataTypeName)
		{
			string fileName = $"{dataTypeName.ToCamelCase()}.json";

			string filePath = $@"{_dataFolderPath}\{fileName}";

			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"File '{fileName}' not found for '{dataTypeName}' type.");
			}

			return filePath;
		}
	}
}
