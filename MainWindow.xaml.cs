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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Grammophone.Indexing;
using Grammophone.Indexing.KernelWeightFunctions;

namespace Grammophone.Indexing.Test
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Command definitions

		public static RoutedUICommand AddCommand = 
			new RoutedUICommand("Add a word to the tree", "Add", typeof(MainWindow));

		public static RoutedUICommand ClearCommand =
			new RoutedUICommand("Clear all words in the tree", "Clear", typeof(MainWindow));

		public static RoutedUICommand MatchCommand =
			new RoutedUICommand("Get matching statistics of a word", "Match", typeof(MainWindow));

		public static RoutedUICommand SearchCommand =
			new RoutedUICommand("Search a word in the tree", "Search", typeof(MainWindow));

		public static RoutedUICommand KernelCommand =
			new RoutedUICommand("Compute a word's kernel value against the tree contents", "Kernel", typeof(MainWindow));

		#endregion

		#region Private fields

		private RadixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData> radixTree;

		#endregion

		#region Construction

		public MainWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Private methods

		#region UI event handling

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			if (!this.CreateNewTree(true)) this.Close();
		}

		private void ExecuteSearch(object sender, ExecutedRoutedEventArgs e)
		{
			var searchWindow = new SearchWindow(this.radixTree);
			searchWindow.Owner = this;
			searchWindow.ShowDialog();
		}

		private void ExecuteAdd(object sender, ExecutedRoutedEventArgs e)
		{
			string text = wordTextBox.Text + "$";

			this.radixTree.AddWord(text.ToArray(), 1.0);

			this.DataContext = null;
			this.DataContext = this.radixTree;
		}

		private void ExecuteClear(object sender, ExecutedRoutedEventArgs e)
		{
			CreateNewTree(true);
		}

		private void ExecutedMatch(object sender, ExecutedRoutedEventArgs e)
		{
			var suffixTree = this.radixTree as SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>;

			if (suffixTree == null) return;

			var matchingStatisticsWindow = new MatchingStatisticsWindow(suffixTree);
			matchingStatisticsWindow.Owner = this;
			matchingStatisticsWindow.ShowDialog();
		}

		private void CanExecuteMatch(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (this.radixTree is SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>);
		}

		private void ExecuteKernel(object sender, ExecutedRoutedEventArgs e)
		{
			var kernelSuffixTree = 
				this.radixTree as KernelSuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>;

			if (kernelSuffixTree == null) return;

			var kernelWindow = new KernelWindow(kernelSuffixTree);

			kernelWindow.Owner = this;

			kernelWindow.ShowDialog();
		}

		private void CanExecuteKernel(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = 
				(this.radixTree is KernelSuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>);
		}

		#endregion

		private bool CreateNewTree(bool allowCancel)
		{
			var treePickWindow = new TreeTypePickWindow(allowCancel);

			treePickWindow.Owner = this;

			treePickWindow.ShowDialog();

			if (treePickWindow.DialogResult.Value)
			{
				switch (treePickWindow.Options.TreeType)
				{
					case TreeTypeEnum.SuffixTree:
						this.radixTree = 
							new SuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>();
						break;

					case TreeTypeEnum.WordTree:
						this.radixTree = 
							new WordTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>();
						break;

					case TreeTypeEnum.KernelSuffixTree:
						{
							WeightFunction weightFunction;

							if (treePickWindow.Options.Exponent == 1.0)
								weightFunction = new SumWeightFunction();
							else
								weightFunction = new ExpSumWeightFunction(treePickWindow.Options.Exponent);

							this.radixTree =
								new KernelSuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData>(weightFunction);
						}
						break;
				}
				this.DataContext = null;
				this.DataContext = this.radixTree;

				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

	}
}
