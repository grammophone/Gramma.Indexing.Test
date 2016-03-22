using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using Gramma.Indexing;

namespace SuffixTreeTest
{
	public class MatchingSubstringConverter : IValueConverter, IMultiValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string word = parameter as string;

			if (word == null) return String.Empty;

			if (!(value is SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>.MatchingStatistics.StatisticsEntry))
				return String.Empty;

			var statisticsEntry =
				(SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>.MatchingStatistics.StatisticsEntry)value;

			return word.Substring(statisticsEntry.WordOffset, statisticsEntry.Length);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IMultiValueConverter Members

		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (values.Length < 2) return String.Empty;

			string word = values[1] as string;

			if (word == null) return String.Empty;

			if (!(values[0] is SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>.MatchingStatistics.StatisticsEntry))
				return String.Empty;

			var statisticsEntry =
				(SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>.MatchingStatistics.StatisticsEntry)values[0];

			return word.Substring(statisticsEntry.WordOffset, statisticsEntry.Length);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
