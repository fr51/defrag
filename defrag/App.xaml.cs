using System;
using System.Windows;

namespace defrag
{
	/// <summary>
	/// app interaction/configuration logic
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// default time elapsed between two user messages
		/// </summary>
		internal static double userMessageInterval=5000d; //default //in milliseconds

		/// <summary>
		/// shows the configuration to the user in the settings window
		/// </summary>
		/// <param name="settingsWindow">
		/// the "<see cref="SettingsWindow"/>" window
		/// </param>
		internal static void ShowConfiguration (SettingsWindow settingsWindow)
		{
			settingsWindow.messageToUserIntervalUpDownControl.CurrentValue=Convert.ToDecimal (userMessageInterval)/1000m;
		}

		/// <summary>
		/// sets the configuration from the settings window's fields
		/// </summary>
		/// <param name="settingsWindow">
		/// the "<see cref="SettingsWindow"/>" window
		/// </param>
		internal static void SaveConfiguration (SettingsWindow settingsWindow)
		{
			userMessageInterval=Convert.ToDouble (settingsWindow.messageToUserIntervalUpDownControl.CurrentValue)*1000d;
		}
	}
}