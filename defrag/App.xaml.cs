using System;
using System.Windows;
using System.Windows.Controls;
using System.Resources;
using System.Reflection;
using System.Linq;

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
		/// current UI language
		/// </summary>
		internal static string userLanguage="fr"; //default

		/// <summary>
		/// fired when the app starts up, but before rendering the main window
		/// </summary>
		/// <param name="sender">
		/// the <see cref="defrag.App"/> app
		/// </param>
		/// <param name="startupEventArgs">
		/// some event-related data
		/// </param>
		private void Application_Startup (object sender, StartupEventArgs startupEventArgs)
		{
			MainWindow mainWindow=new MainWindow ();

			mainWindow.ShowDialog ();
		}

		/// <summary>
		/// shows the configuration to the user in the settings window
		/// </summary>
		/// <param name="settingsWindow">
		/// the "<see cref="SettingsWindow"/>" window
		/// </param>
		internal static void ShowConfiguration (SettingsWindow settingsWindow)
		{
			settingsWindow.messageToUserIntervalUpDownControl.CurrentValue=Convert.ToDecimal (userMessageInterval)/1000m;
			((ComboBoxItem) settingsWindow.LanguageSelectionList.FindName (userLanguage)).IsSelected=true;
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

			ComboBox languagesList=settingsWindow.LanguageSelectionList;
			ComboBoxItem selectedLanguage=(ComboBoxItem) languagesList.SelectedItem;
			userLanguage=selectedLanguage.Name;
			((MainWindow) App.Current.MainWindow.FindName ("mainWindow")).SetStrings ();
		}

		/// <summary>
		/// gets the resources manager in order to retrieve strings
		/// </summary>
		/// <returns>
		/// the resource manager
		/// </returns>
		internal static ResourceManager GetStringsResourceManager ()
		{
			ResourceManager resourceManager=new ResourceManager ("defrag.appStrings."+userLanguage, Assembly.GetExecutingAssembly ());

			return (resourceManager);
		}
	}
}