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
using Gramma.Indexing;

namespace Gramma.Indexing.Test
{
	/// <summary>
	/// Interaction logic for SearchWindow.xaml
	/// </summary>
	public partial class SearchWindow : Window
	{
		#region Command definitions

		public static RoutedUICommand SearchCommand =
			new RoutedUICommand("Search", "Search", typeof(SearchWindow));

		#endregion

		#region Auxilliary types

		public class SearchOptions
		{
			public SearchOptions()
			{
				this.Word = String.Empty;
				this.MaxDistance = 2.0;
			}

			public double MaxDistance { get; set; }
			public string Word { get; set; }
		}

		#endregion

		#region Private fields

		private RadixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData> radixTree;

		#endregion

		#region Construction

		public SearchWindow(RadixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData> radixTree)
		{
			if (radixTree == null) throw new ArgumentNullException("radixTree");

			this.radixTree = radixTree;

			this.Options = new SearchOptions();
			this.Results = new List<RadixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>.SearchResult>();

			InitializeComponent();

			this.DataContext = this;
		}

		#endregion

		#region Public properties

		public SearchOptions Options { get; private set; }

		public IList<RadixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>.SearchResult> Results { get; private set; }

		#endregion

		#region Private methods

		private void CanExecuteSearch(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = BindingHelper.AreAllValidated(this);
		}

		private void ExecuteSearch(object sender, ExecutedRoutedEventArgs e)
		{
			if (!BindingHelper.AreAllValidated(this)) return;

			string word = this.Options.Word + "$";

			this.Results =
				this.radixTree.ApproximateSearch(
					word.ToCharArray(), 
					this.Options.MaxDistance,
					delegate(char chr1, char chr2)
					{
						return chr1 == chr2 ? 0.0 : 1.0;
					}
				);

			this.resultsListView.DataContext = null;
			this.resultsListView.DataContext = this;
		}

		#endregion
	}
}
