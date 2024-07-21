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
		/// fired when the "legendWindowOKButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void legendWindowOKButton_Click (object sender, RoutedEventArgs e)
		{
			this.Close ();
		}
	}
}