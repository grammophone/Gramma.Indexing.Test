using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Gramma.Indexing.Test
{
	public class CharArrayConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			char[] array = value as char[];

			if (array == null) throw new ArgumentException("value is not array of characters.", "value");

			if (targetType != typeof(string))
				throw new ArgumentException("targetType is not string.", "targetType");

			return StringFromArray(array);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Implementation

		private static string StringFromArray(char[] array)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(array);

			return builder.ToString();
		}

		#endregion
	}
}
