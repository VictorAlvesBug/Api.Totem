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
			if (list is null || !list.SafeAny())
				return default;

			var random = new Random();
			var index = random.Next(list.Count() - 1);

			return list.ToArray()[index];
		}

		public static IEnumerable<T> PickManyRandomly<T>(this IEnumerable<T> list, int? amount = null)
		{
			if (list is null || !list.SafeAny())
				return default;

			var auxList = list.ToList();
			var resultList = new List<T>();
			var random = new Random();
			var itemsAmount = amount ?? random.Next(1, auxList.Count);

            for (int i = 0; i < Math.Min(itemsAmount, auxList.Count); i++)
			{
				var index = random.Next(auxList.Count - 1);
				resultList.Add(auxList[index]);
				auxList.RemoveAt(index);
			}

			return resultList;
		}

		public static bool HasSameItems<T>(this IEnumerable<T> listA, IEnumerable<T> listB)
		{
			if (listA is null || listB is null)
				return false;

			return listA.Count() == listB.Count() && !listA.Except(listB).Any();
		}
	}
}
