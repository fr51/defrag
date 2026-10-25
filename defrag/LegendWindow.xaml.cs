using System;
using System.Windows;
using System.Resources;

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

		/// <summary>
		/// fired once the window's content is rendered
		/// </summary>
		/// <param name="sender">
		/// the <see cref="LegendWindow"> window
		/// </param>
		/// <param name="eventArgs">
		/// some event-related data
		/// </param>
		private void LegendWindow_ContentRendered (object sender, EventArgs eventArgs)
		{
			this.SetStrings ();
		}

		/// <summary>
		/// sets the strings used in this window, according to the language the user chose
		/// </summary>
		private void SetStrings ()
		{
			ResourceManager resourceManager=App.GetStringsResourceManager ();

			this.Title=resourceManager.GetString ("LegendWindowTitle");
			this.FreeBlockLabel.Content=resourceManager.GetString ("LegendWindowFreeBlockLabel");
			this.FaultyBlockLabel.Content=resourceManager.GetString ("LegendWindowFaultyBlockLabel");
			this.DefragmentedBlockLabel.Content=resourceManager.GetString ("LegendWindowDefragmentedBlockLabel");
			this.FragmentedBlockLabel.Content=resourceManager.GetString ("LegendWindowFragmentedBlockLabel");
			this.UnmovableBlockLabel.Content=resourceManager.GetString ("LegendWindowUnmovableBlockLabel");
		}
	}
}