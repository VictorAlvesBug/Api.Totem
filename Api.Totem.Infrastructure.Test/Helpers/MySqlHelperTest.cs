using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Helpers;
using Google.Protobuf.WellKnownTypes;
using System.Drawing;

namespace Api.Totem.Infrastructure.Test.Helpers
{
	public class MySqlHelperTest
	{
		[Fact]
		public void ConvertDynamicToStringTest()
		{
			var dictStringDynamic = new Dictionary<string, dynamic>()
			{
				{ "1111111", 1111111 },
				{ "2222222", (long)2222222 },
				{ "333.333", 333.333 },
				{ "444.444", (float)444.444 },
				{ "555.555", (decimal)555.555 },
				{ "666.666", (float)666.666 },
				{ "777", (short)777 },
				{ "255", (byte) 255 },
				{ "'lorem'", "lorem" },
				{ "'C'", 'C' },
				{ "TRUE", true },
				{ "FALSE", false },
				{ "'1999-01-10 00:00:00.000'", new DateTime(1999, 01, 10) },
				{ "'1999-01-10 01:05:00.000'", new DateTime(1999, 01, 10, 01, 05, 00) },
				{ "'2024-09-21 16:41:44.127'", new DateTime(2024, 09, 21, 16, 41, 44, 127) },
				{ "'1 02:03:04.005'", new TimeSpan(1, 2, 3, 4, 5) },
				{ "'364 23:59:59.999'", new TimeSpan(364, 23, 59, 59, 999) },
				{ "'{\"Name\":\"Victor\",\"Age\":25}'", new { Name = "Victor", Age = 25 } },
			};

			var guid = Guid.NewGuid();

			dictStringDynamic.Add($"'{guid}'", guid);

			foreach (var kvp in dictStringDynamic)
			{
				var expectedString = kvp.Key;
				var dynamicValue = kvp.Value;

				var actualString = MySqlHelper.ConvertDynamicToString(dynamicValue);

				Assert.Equal(expectedString, actualString);
			}
		}
	}
}