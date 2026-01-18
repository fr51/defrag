using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;

namespace defrag
{
	/// <summary>
	/// interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// available colors to fill the blocks
		/// </summary>
		/// <remarks>
		/// See the readme ("code couleur" section) for details
		/// </remarks>
		private readonly Color [] availableColors=new Color [5];

		/// <summary>
		/// boolean used to halt the defragmentation
		/// </summary>
		private bool IsHalted=false;

		/// <summary>
		/// constructor
		/// </summary>
		public MainWindow ()
		{
			InitializeComponent ();

			this.GatherColors ();
		}

		/// <summary>
		/// fired when the window is ready for interaction
		/// </summary>
		/// <param name="sender">
		/// the <see cref="MainWindow"> window
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void MainWindow_Loaded (object sender, RoutedEventArgs routedEventArgs)
		{
			this.PopulateTopPane ();
		}

		/// <summary>
		/// fired once the window's content is rendered
		/// </summary>
		/// <param name="sender">
		/// the <see cref="MainWindow"> window
		/// </param>
		/// <param name="eventArgs">
		/// some event-related data
		/// </param>
		private void MainWindow_ContentRendered (object sender, EventArgs eventArgs)
		{
			this.StartDefragmentation ();
		}

		/// <summary>
		/// fired when the "<see cref="StopButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the <see cref="StopButton"> button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void StopButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			this.StopDefragmentation ();
		}

		/// <summary>
		/// Fills the "<see cref="availableColors"/>" array
		/// </summary>
		private void GatherColors ()
		{
			this.availableColors [0]=Color.FromRgb (255, 255, 255);
			this.availableColors [1]=Color.FromRgb (255, 0, 0);
			this.availableColors [2]=Color.FromRgb (0, 0, 255);
			this.availableColors [3]=Color.FromRgb (0, 255, 255);
			this.availableColors [4]=Color.FromRgb (0, 0, 0);
		}

		/// <summary>
		/// Populates the "<see cref="topPane"/>" canvas with the randomly-filled (among available colors) blocks
		/// </summary>
		private void PopulateTopPane ()
		{
			for (int i=0; i<this.topPane.Height/25; i++) //showing 20 lines
			{
				for (int j=0; j<(this.topPane.Width/15.38)-1.0d; j++) //showing (nearly) 52 columns
				{
					Rectangle rectangle=new Rectangle
					{
						Height=20,
						Width=10,
						VerticalAlignment=VerticalAlignment.Top,
						HorizontalAlignment=HorizontalAlignment.Left,
						Margin=new Thickness (15*j+5, 25*i+5, 0, 0),
						Stroke=null
					};

					this.FillBlock (rectangle);
					this.topPane.Children.Add (rectangle);
				}
			}
		}

		/// <summary>
		/// Fills a block with a random color (among the available ones)
		/// </summary>
		/// <param name="block">
		/// the block to fill
		/// </param>
		private void FillBlock (Rectangle block)
		{
			Random colorPickingGenerator=new Random ();
			block.Fill=new SolidColorBrush (this.availableColors [colorPickingGenerator.Next (0, this.availableColors.Length)]);
		}

		/// <summary>
		/// Starts the defragmentation by randomly filling the blocks then waiting half a second
		/// </summary>
		private async void StartDefragmentation ()
		{
			Random randomNumbersGenerator=new Random ();

			await Task.Run (() =>
			{
				while (true)
				{
					if (this.IsHalted==false)
					{
						Application.Current.Dispatcher.Invoke (() => //An exception may pop up here when the "MainWindow" window is closing. It doesn't matter as we exit anyway
						{
							for (int i=0; i<this.topPane.Children.Count; i++)
							{
								((Rectangle) this.topPane.Children [i]).Fill=new SolidColorBrush (this.availableColors [randomNumbersGenerator.Next (0, this.availableColors.Length)]);
							}
						});

						Thread.Sleep (500);
					}
					else
					{
						return;
					}
				}
			});
		}

		/// <summary>
		/// stops the defragmentation
		/// </summary>
		private void StopDefragmentation ()
		{
			this.IsHalted=true;
			this.StopButton.IsEnabled=false;
		}
	}
}