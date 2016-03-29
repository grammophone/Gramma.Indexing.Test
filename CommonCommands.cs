using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Gramma.Indexing.Test
{
	public static class CommonCommands
	{
		public static RoutedUICommand OkCommand =
			new RoutedUICommand("Accept selection", "Ok", typeof(CommonCommands));

		public static RoutedUICommand CancelCommand =
			new RoutedUICommand("Cancel selection", "Cancel", typeof(CommonCommands));

	}
}
