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
	/// Interaction logic for KernelWindow.xaml
	/// </summary>
	public partial class KernelWindow : Window
	{
		#region Command definitions

		public static RoutedUICommand ComputeCommand =
			new RoutedUICommand("Compute the kernel", "Compute", typeof(KernelWindow));

		#endregion

		#region Private fields

		private KernelSuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData> kernelSuffixTree;

		#endregion

		#region Construction

		public KernelWindow(KernelSuffixTree<char, KernelDataTypes.WordItem, KernelDataTypes.NodeData> kernelSuffixTree)
		{
			if (kernelSuffixTree == null) throw new ArgumentNullException("kernelSuffixTree");

			this.kernelSuffixTree = kernelSuffixTree;

			this.InputString = String.Empty;

			this.DataContext = this;

			InitializeComponent();
		}

		#endregion

		#region Public properties

		public string InputString { get; set; }

		public double KernelValue { get; set; }

		#endregion

		#region Private methods

		private void ExecuteCompute(object sender, ExecutedRoutedEventArgs e)
		{
			var inputString = this.InputString + "$";

			this.KernelValue = 
				this.kernelSuffixTree.ComputeKernel(inputString.ToCharArray());

			this.DataContext = null;
			this.DataContext = this;
		}

		#endregion
	}
}
