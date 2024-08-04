namespace Api.Totem.Helpers.Extensions
{
	public static class IEnumerableExtensions
	{
		public static string JoinThis(this IEnumerable<string> list, string separator = ",") =>
			string.Join(separator, list);

		public static bool SafeAny<T>(this IEnumerable<T> list, Func<T, bool>? predicate = null)
		{
			if (list is null)
				return false;

			if (predicate is null)
				return list.Any();

			return list.Any(predicate);
		}

		public static T PickOneRandomly<T>(this IEnumerable<T> list)
		{
			if (list is null)
				return default;

			var random = new Random();
			var index = random.Next(list.Count() - 1);

			return list.ToArray()[index];
		}
	}
}
