using System.Windows;

namespace defrag
{
	/// <summary>
	/// app interaction/configuration logic
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// time elapsed between two user messages
		/// </summary>
		internal static double userMessageInterval=5000d; //in milliseconds
		internal static readonly int userMessageIntervalMinValue=1000;
		internal static readonly int userMessageIntervalMaxValue=300000;

		/// <summary>
		/// shows the configuration to the user in the settings window
		/// </summary>
		/// <param name="settingsWindow">
		/// the settings window
		/// </param>
		internal static void showConfiguration (SettingsWindow settingsWindow)
		{
			settingsWindow.messageToUserIntervalTextBox.Text=userMessageInterval.ToString ();
		}

		/// <summary>
		/// sets the configuration from the settings window's fields
		/// </summary>
		/// <param name="settingsWindow">
		/// the settings window
		/// </param>
		internal static void saveConfiguration (SettingsWindow settingsWindow)
		{
			userMessageInterval=double.Parse (settingsWindow.messageToUserIntervalTextBox.Text);
		}
	}
}