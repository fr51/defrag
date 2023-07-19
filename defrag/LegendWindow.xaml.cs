using System.Windows;

namespace defrag
{
	/// <summary>
	/// interaction logic for LegendWindow.xaml
	/// </summary>
	public partial class LegendWindow : Window
	{
		/// <summary>
		/// constructor
		/// </summary>
		public LegendWindow ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// fired when the "OKButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void OKButton_Click (object sender, RoutedEventArgs e)
		{
			this.Close ();
		}
	}
}