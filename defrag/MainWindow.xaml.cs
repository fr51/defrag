using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

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
		/// see the readme ("code couleur" section) for details
		/// </remarks>
		private Color [] availableColors=new Color [5];

		/// <summary>
		/// boolean used to pause the defragmentation
		/// </summary>
		private bool isPaused=false;

		/// <summary>
		/// boolean used to halt the defragmentation
		/// </summary>
		private bool isHalted=false;

		/// <summary>
		/// constructor
		/// </summary>
		public MainWindow ()
		{
			InitializeComponent ();

			this.gatherColors ();
		}

		/// <summary>
		/// fired when the window is ready for interaction
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void mainWindow_Loaded (object sender, RoutedEventArgs e)
		{
			this.populateTopPane ();
		}

		/// <summary>
		/// fired once the window's content is rendered
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void mainWindow_ContentRendered (object sender, EventArgs e)
		{
			this.startDefragmentation ();
		}

		/// <summary>
		/// populates the "topPane" canvas with the randomly-filled (among the available colors) blocks
		/// </summary>
		private void populateTopPane ()
		{
			for (int i=0; i<this.topPane.Height/25; i++) //showing 20 lines
			{
				for (int j=0; j<(this.topPane.Width/15.38)-1.0d; j++) //showing (nearly) 52 columns
				{
					Rectangle r=new Rectangle ();
					r.Height=20;
					r.Width=10;
					r.VerticalAlignment=VerticalAlignment.Top;
					r.HorizontalAlignment=HorizontalAlignment.Left;
					r.Margin=new Thickness (15*j+5, 25*i+5, 0, 0);
					r.Stroke=null;

					this.fillBlock (r);
					this.topPane.Children.Add (r);
				}
			}
		}

		/// <summary>
		/// starts the defragmentation by randomly filling the blocks then waiting half a second
		/// </summary>
		private async void startDefragmentation ()
		{
			Random randomNumbersGenerator=new Random ();
			Random progressValuesGenerator=new Random ();

			await Task.Run (() =>
			{
				while (true)
				{
					if (this.isHalted==false)
					{
						Application.Current.Dispatcher.Invoke (() =>
						{
							if (this.isPaused==false)
							{
								for (int i=0; i<this.topPane.Children.Count; i++)
								{
									((Rectangle) this.topPane.Children [i]).Fill=new SolidColorBrush (this.availableColors [randomNumbersGenerator.Next (0, this.availableColors.Length)]);
								}

								int progressValue=progressValuesGenerator.Next (0, 101);
								this.progressBar.Value=progressValue;
								this.progressLabel.Content=Regex.Replace (this.progressLabel.Content.ToString (), "[0-9]{1,3}", progressValue.ToString ());
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
		/// fills the "availableColors" array
		/// </summary>
		private void gatherColors ()
		{
			this.availableColors [0]=Color.FromRgb (255, 255, 255);
			this.availableColors [1]=Color.FromRgb (255, 0, 0);
			this.availableColors [2]=Color.FromRgb (0, 0, 255);
			this.availableColors [3]=Color.FromRgb (0, 255, 255);
			this.availableColors [4]=Color.FromRgb (0, 0, 0);
		}

		/// <summary>
		/// fills a block with a random color (among the available ones)
		/// </summary>
		/// <param name="block">
		/// the block to fill
		/// </param>
		private void fillBlock (Rectangle block)
		{
			Random generatorForPickingAColor=new Random ();
			block.Fill=new SolidColorBrush (this.availableColors [generatorForPickingAColor.Next (0, this.availableColors.Length)]);
		}

		/// <summary>
		/// fired when the "pauseButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void pauseButton_Click (object sender, RoutedEventArgs e)
		{
			if (this.isPaused==false)
			{
				this.isPaused=true;
				this.pauseButton.Content="Reprendre";
			}
			else
			{
				this.isPaused=false;
				this.pauseButton.Content="Pause";
			}
		}

		/// <summary>
		/// fired when the "stopButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void stopButton_Click (object sender, RoutedEventArgs e)
		{
			this.pauseButton.IsEnabled=false;
			this.progressLabel.Content=this.progressLabel.Content.ToString ().Replace ("en cours", "arrêtée");
			this.isHalted=true;
			this.stopButton.IsEnabled=false;
		}
	}
}