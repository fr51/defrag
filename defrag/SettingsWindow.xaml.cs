using System.Windows;

namespace defrag
{
	/// <summary>
	/// interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
		/// <summary>
		/// constructor
		/// </summary>
		public SettingsWindow ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// fired when the "<see cref="SettingsWindowOKButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the "<see cref="SettingsWindowOKButton"/" button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void SettingsWindowOKButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			App.SaveConfiguration (this);

			this.DialogResult=true;

			this.Close ();
		}

		/// <summary>
		/// fired when the "<see cref="SettingsWindowCancelButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the "<see cref="SettingsWindowCancelButton"/>" button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void SettingsWindowCancelButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			this.DialogResult=false;

			this.Close ();
		}

		/// <summary>
		/// fired when the window is ready for interaction
		/// </summary>
		/// <param name="sender">
		/// the "<see cref="SettingsWindow"/>" window
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void SettingsWindow_Loaded (object sender, RoutedEventArgs routedEventArgs)
		{
			App.ShowConfiguration (this);
		}
	}
}