using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Timers;
using System.Collections.Generic;

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
		/// timer used to greet the user
		/// </summary>
		/// <remarks>
		/// this only toggles the "<see cref="toggleUserGreeting"/>" boolean via the "<see cref="allowToGreetUser"/>" method
		/// </remarks>
		private System.Timers.Timer timer=new System.Timers.Timer (); //duration in milliseconds

		/// <summary>
		/// boolean used to greet the user
		/// </summary>
		private bool toggleUserGreeting=false;

		/// <summary>
		/// constructor
		/// </summary>
		public MainWindow ()
		{
			InitializeComponent ();

			this.gatherColors ();
			this.setTimerInterval (App.userMessageInterval);

			this.timer.Elapsed+=this.allowToGreetUser;
			this.timer.AutoReset=true;
		}

		/// <summary>
		/// sets the timer's interval (used to greet the user)
		/// </summary>
		/// <param name="interval">
		/// the timer's interval
		/// </param>
		private void setTimerInterval (double interval)
		{
			this.timer.Interval=interval;
		}

		/// <summary>
		/// toggles a boolean allowing to greet the user
		/// </summary>
		/// <param name="source">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void allowToGreetUser (object source, ElapsedEventArgs e)
		{
			this.toggleUserGreeting=true;
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
				this.timer.Start ();

				while (true)
				{
					if (this.isHalted==false)
					{
						Application.Current.Dispatcher.Invoke (() =>
						{
							if (this.isPaused==false)
							{
								if (this.toggleUserGreeting==true)
								{
									this.greetUser ();
									this.toggleUserGreeting=false;
								}
								else
								{
									for (int i=0; i<this.topPane.Children.Count; i++)
									{
										((Rectangle) this.topPane.Children [i]).Fill=new SolidColorBrush (this.availableColors [randomNumbersGenerator.Next (0, this.availableColors.Length)]);
									}
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
		/// pauses the defragmentation
		/// </summary>
		private void pauseDefragmentation ()
		{
			this.isPaused=true;
			this.pauseButton.Content="Reprendre";

			this.timer.Stop ();
		}

		/// <summary>
		/// resumes the defragmentation
		/// </summary>
		private void resumeDefragmentation ()
		{
			this.isPaused=false;
			this.pauseButton.Content="Pause";

			this.timer.Start ();
		}

		/// <summary>
		/// stops the defragmentation
		/// </summary>
		private void stopDefragmentation ()
		{
			this.pauseButton.IsEnabled=false;
			this.progressLabel.Content=this.progressLabel.Content.ToString ().Replace ("en cours", "arrêtée");
			this.isHalted=true;
			this.stopButton.IsEnabled=false;

			this.timer.Stop ();
		}

		/// <summary>
		/// fills the "<see cref="availableColors"/>" array
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
				this.pauseDefragmentation ();
			}
			else
			{
				this.resumeDefragmentation ();
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
			this.stopDefragmentation ();
		}

		/// <summary>
		/// fired when the "detailsButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void detailsButton_Click (object sender, RoutedEventArgs e)
		{
			if (this.progressLabel.IsVisible)
			{
				this.progressLabel.Visibility=Visibility.Hidden;
				this.progressBar.Visibility=Visibility.Hidden;
			}
			else
			{
				this.progressLabel.Visibility=Visibility.Visible;
				this.progressBar.Visibility=Visibility.Visible;
			}
		}

		/// <summary>
		/// fired when the "legendButton" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void legendButton_Click (object sender, RoutedEventArgs e)
		{
			LegendWindow legendWindow=new LegendWindow ();
			legendWindow.Owner=this;

			legendWindow.ShowDialog ();
		}

		/// <summary>
		/// fired when the "<see cref="settingsButton"/>" is clicked
		/// </summary>
		/// <param name="sender">
		/// the object that fires the event
		/// </param>
		/// <param name="e">
		/// some event-related data
		/// </param>
		private void settingsButton_Click (object sender, RoutedEventArgs e)
		{
			SettingsWindow settingsWindow=new SettingsWindow ();
			settingsWindow.Owner=this;
			bool? b=settingsWindow.ShowDialog ();

			if (b==true)
			{
				this.pauseDefragmentation ();
				this.setTimerInterval (App.userMessageInterval);
				this.resumeDefragmentation ();
			}
		}

		/// <summary>
		/// greets the user by showing a friendly visual message
		/// </summary>
		/// <remarks>
		/// The message consists in a right hand shown from the rear. The blocks to show are arbitrarily determined in the "<see cref="getBlockIndexesInOrderToGreetUser"/>" method
		/// </remarks>
		private void greetUser ()
		{
			for (int i=0; i<this.topPane.Children.Count; i++)
			{
				((Rectangle) this.topPane.Children [i]).Fill=this.topPane.Background;
			}

			List <int> blockIndexes=new List <int> ();

			this.getBlockIndexesInOrderToGreetUser (blockIndexes);

			for (int i=0; i<blockIndexes.Count; i++) //filling the required blocks
			{
				((Rectangle) this.topPane.Children [blockIndexes [i]]).Fill=new SolidColorBrush (this.availableColors [1]); //in red
			}
		}

		/// <summary>
		/// determines which blocks to show when greeting the user
		/// </summary>
		/// <param name="blockIndexes">
		/// the indexes list used by the "<see cref="greetUser"/>" method
		/// </param>
		/// <remarks>
		/// I chose to write this in a separate method in a readability purpose
		/// </remarks>
		private void getBlockIndexesInOrderToGreetUser (List <int> blockIndexes)
		{
			int index=77;

			//The middle finger is most important one. Did I say the message is friendly?
			for (int i=0; i<6; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=78;

			for (int i=0; i<6; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//index
			index=179;

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=180;

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//ring finger
			index=183;

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=184;

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//pinky
			index=237;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=238;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//thumb
			index=386;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index-=52;
				index--;
			}

			index=385;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index-=52;
				index--;
			}

			blockIndexes.Add (334);

			//rest of the hand
			for (int i=1; i<9; i++) //bottom
			{
				blockIndexes.Add (446+52-1*i);
			}

			blockIndexes.Add (437); //left side
			blockIndexes.Add (446); //right side

			//remaining space
			for (int i=387; i<395; i++) //part 1
			{
				blockIndexes.Add (i);
			}

			for (int i=438; i<446; i++) //part 2
			{
				blockIndexes.Add (i);
			}
		}
	}
}