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
		/// fired when the "<see cref="LegendWindowOKButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the "<see cref="LegendWindowOKButton"/>" button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void LegendWindowOKButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			this.DialogResult=true;

			this.Close ();
		}
	}
}