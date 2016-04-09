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

using Grammophone.Windows;

namespace Grammophone.Indexing.Test
{
	/// <summary>
	/// Interaction logic for TreeTypePickWindow.xaml
	/// </summary>
	public partial class TreeTypePickWindow : Window
	{
		#region Types definition

		public class TreeOptions
		{
			public TreeOptions()
			{
				this.TreeType = TreeTypeEnum.SuffixTree;
				this.Exponent = 1.0;
			}

			public TreeOptions(TreeTypeEnum treeType, double exponent = 1.0)
			{
				this.TreeType = treeType;
				this.Exponent = exponent;
			}

			public TreeTypeEnum TreeType { get; set; }

			public double Exponent { get; set; }
		}

		#endregion

		#region Construction

		public TreeTypePickWindow(bool allowCancel = true)
		{
			InitializeComponent();

			this.suffixTreeRadioButton.IsChecked = true;
			this.AllowCancel = allowCancel;
			this.Options = new TreeOptions();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			this.DataContext = this.Options;
		}

		#endregion

		#region Public properties

		public TreeOptions Options { get; private set; }

		public bool AllowCancel { get; private set; }

		#endregion

		#region Private methods

		private void ExecutedOk(object sender, ExecutedRoutedEventArgs e)
		{
			if (this.Options.TreeType == TreeTypeEnum.KernelSuffixTree && !exponentTextBox.AreAllValidated())
			{
				return;
			}

			this.DialogResult = true;
			this.Close();
		}

		private void ExecuteCancel(object sender, ExecutedRoutedEventArgs e)
		{
			this.Close();
		}

		private void CanExecuteCancel(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = this.AllowCancel;
		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if (!this.AllowCancel && (!this.DialogResult.HasValue || !this.DialogResult.Value))
			{
				e.Cancel = true;
				return;
			}
			base.OnClosing(e);
		}

		#endregion

	}
}
