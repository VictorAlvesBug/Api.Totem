using Newtonsoft.Json;

namespace Api.Totem.Helpers.Extensions
{
	public static class ObjectExtensions
	{
		public static string SerializeToJson(this object obj)
		{
			if (obj == null)
				return null;

			return JsonConvert.SerializeObject(obj);
		}


		public static TReturn DeserializeFromJson<TReturn>(this string json)
		{
			if (string.IsNullOrEmpty(json))
				return default;

			return JsonConvert.DeserializeObject<TReturn>(json) ?? default;
		}

		public static TReturn ConvertTo<TReturn>(this object obj)
		{
			return obj.SerializeToJson().DeserializeFromJson<TReturn>();
		}

		public static IEnumerable<TReturn> ConvertTo<TReturn>(this IEnumerable<object> list)
		{
			if (!list.SafeAny())
				return new List<TReturn>();

			var result = new List<TReturn>();

			return list.Select(obj => obj.ConvertTo<TReturn>());
		}
	}
}
