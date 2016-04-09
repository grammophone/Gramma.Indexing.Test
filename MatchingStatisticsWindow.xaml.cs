using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Grammophone.Indexing;

namespace Grammophone.Indexing.Test
{
	/// <summary>
	/// Interaction logic for MatchingStatisticsWindow.xaml
	/// </summary>
	public partial class MatchingStatisticsWindow : Window
	{
		#region Private fields

		private SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData> suffixTree;

		#endregion

		#region Command definitions

		public static RoutedUICommand ExecuteCommand =
			new RoutedUICommand("Get the matching statistics", "Execute", typeof(MatchingStatisticsWindow));

		#endregion

		#region Public properties

		public string CurrentWord { get; private set; }

		#endregion

		#region Construction

		public MatchingStatisticsWindow(SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData> suffixTree)
		{
			if (suffixTree == null) throw new ArgumentNullException("suffixTree");
			this.suffixTree = suffixTree;
			this.CurrentWord = String.Empty;

			InitializeComponent();
		}

		#endregion

		#region Private methods

		private void ExecuteExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.CurrentWord = this.wordTextBox.Text;
			char[] word = this.wordTextBox.Text.ToArray();

			var matchingStatistics = this.suffixTree.GetMatchingStatistics(word);

			this.DataContext = matchingStatistics;
		}

		private void CanExecuteExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (this.wordTextBox.Text.Length > 0);
		}

		#endregion

	}
}
