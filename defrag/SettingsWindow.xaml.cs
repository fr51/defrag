using System.Windows;
using System.Windows.Controls;
using System;
using System.Resources;

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

		/// <summary>
		/// fired once the window's content is rendered
		/// </summary>
		/// <param name="sender">
		/// the <see cref="SettingsWindow"> window
		/// </param>
		/// <param name="eventArgs">
		/// some event-related data
		/// </param>
		private void SettingsWindow_ContentRendered (object sender, EventArgs eventArgs)
		{
			this.SetStrings ();
		}

		// <summary>
		/// sets the strings used in this window, according to the language the user chose
		/// </summary>
		private void SetStrings ()
		{
			ResourceManager resourceManager=App.GetStringsResourceManager ();

			this.Title=resourceManager.GetString ("SettingsWindowTitle");
			this.messageToUserIntervalLabel.Text=resourceManager.GetString ("SettingsWindowmessageToUserIntervalLabel");
			this.messageToUserIntervalUpDownControl.ToolTip=resourceManager.GetString ("SettingsWindowmessageToUserIntervalUpDownControlTooltip");
			this.LanguageSelectionLabel.Content=resourceManager.GetString ("SettingsWindowLanguageSelectionLabel");
			this.SettingsWindowOKButton.Content=resourceManager.GetString ("SettingsWindowOKButton");
			this.SettingsWindowCancelButton.Content=resourceManager.GetString ("SettingsWindowCancelButton");
		}
	}
}