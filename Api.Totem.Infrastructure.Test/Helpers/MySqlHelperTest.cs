using Api.Totem.Infrastructure.Helpers;
using Google.Protobuf.WellKnownTypes;

namespace Api.Totem.Infrastructure.Test.Helpers
{
	public class MySqlHelperTest
	{
		[Theory]
		[InlineData(123, "123")]
		[InlineData(123456789L, "123456789")]
		[InlineData(12345.67, "12345.67")]
		[InlineData(12345.67f, "12345.67")]
		[InlineData("String", "'String'")]
		[InlineData('C', "'C'")]
		[InlineData(true, "TRUE")]
		[InlineData(false, "FALSE")]
		public void ConvertDynamicToStringTest(dynamic dynamicValue, string expectedString)
		{
			var actualString = MySqlHelper.ConvertDynamicToString(dynamicValue);

			Assert.Equal(expectedString, actualString);
		}

		[Theory]
		[InlineData(12345.67, "12345.67")]
		[InlineData(12345.678, "12345.678")]
		public void ConvertDynamicDecimalToStringTest(dynamic dynamicValue, string expectedString)
		{
			var actualString = MySqlHelper.ConvertDynamicToString((decimal)dynamicValue);

			Assert.Equal(expectedString, actualString);
		}

		[Theory]
		[InlineData("1999-01-10", "1999-01-10 00:00:00")]
		[InlineData("1999-01-10 01:05:00", "1999-01-10 01:05:00")]
		public void ConvertDynamicDateTimeToStringTest(string dateValue, string expectedString)
		{
			var actualString = MySqlHelper.ConvertDynamicToString(Convert.ToDateTime(dateValue));

			Assert.Equal(expectedString, actualString);
		}

		/*[Theory]
		[InlineData("1999-01-10", "1999-01-10")]
		[InlineData("1999-01-10 01:05", "1999-01-10 01:05")]
		public void ConvertDynamicTimeSpanToStringTest(string dateValue, string expectedString)
		{
			var actualString = MySqlHelper.ConvertDynamicToString(new TimeSpan(dateValue));

			Assert.Equal(expectedString, actualString);
		}*/
	}
}