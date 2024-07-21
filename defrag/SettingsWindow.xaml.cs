using System.Windows;
using System.Text.RegularExpressions;

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
		/// fired when the "settingsWindowOKButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void settingsWindowOKButton_Click (object sender, RoutedEventArgs e)
		{
			if (this.checkConfiguration ()==true)
			{
				App.saveConfiguration (this);

				this.DialogResult=true;

				this.Close ();
			}
		}

		/// <summary>
		/// fired when the "settingsWindowCancelButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void settingsWindowCancelButton_Click (object sender, RoutedEventArgs e)
		{
			this.Close ();
		}

		/// <summary>
		/// fired when the "settingsWindow" window is loaded (ready for interaction)
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void settingsWindow_Loaded (object sender, RoutedEventArgs e)
		{
			App.showConfiguration (this);
		}

		/// <summary>
		/// checks the fields input validity
		/// </summary>
		/// <returns>
		/// whether the configuration is OK or not
		/// </returns>
		private bool checkConfiguration ()
		{
			if (this.checkUserMessageInterval ()==false)
			{
				return (false);
			}

			return (true);
		}

		/// <summary>
		/// checks the user message interval
		/// </summary>
		/// <returns>
		/// whether the user message interval is valid
		/// </returns>
		private bool checkUserMessageInterval ()
		{
			if (Regex.IsMatch (this.messageToUserIntervalTextBox.Text, "^[0-9]{4,6}$")==true)
			{
				int i=int.Parse (this.messageToUserIntervalTextBox.Text);

				if (i<App.userMessageIntervalMinValue || i>App.userMessageIntervalMaxValue)
				{
					this.warnUser ("L'intervalle doit être compris entre "+App.userMessageIntervalMinValue+" et "+App.userMessageIntervalMaxValue);

					return (false);
				}
			}
			else
			{
				this.warnUser ("L'intervalle doit être un nombre entier positif");

				return (false);
			}

			return (true);
		}

		/// <summary>
		/// shows a real message to the user (related to configuration)
		/// </summary>
		/// <param name="message">
		/// the message text
		/// </param>
		private void warnUser (string message)
		{
			MessageBox.Show (message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}