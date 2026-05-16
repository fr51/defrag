using System;
using System.Collections.Generic;
using System.Linq;
using Timers=System.Timers; //without alias, the compiler confuses with System.Threading namespace when using the Timer class
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
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
		/// boolean used to pause the defragmentation
		/// </summary>
		private bool IsPaused=false;

		/// <summary>
		/// timer used to greet the user
		/// </summary>
		/// <remarks>
		/// this only toggles the "<see cref="ToggleUserGreeting"/>" boolean via the "<see cref="AllowToGreetUser"/>" method
		/// </remarks>
		private readonly Timers.Timer Timer=new Timers.Timer (5000d); //duration in milliseconds

		/// <summary>
		/// boolean used to greet the user
		/// </summary>
		private bool ToggleUserGreeting=false;

		/// <summary>
		/// constructor
		/// </summary>
		public MainWindow ()
		{
			InitializeComponent ();

			this.GatherColors ();

			this.Timer.Elapsed+=this.AllowToGreetUser;
			this.Timer.AutoReset=true;
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
		/// fired when the "<see cref="PauseButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the <see cref="PauseButton"> button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void PauseButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			if (this.IsPaused==false) //running
			{
				this.PauseDefragmentation ();
			}
			else //paused
			{
				this.ResumeDefragmentation ();
			}
		}

		/// <summary>
		/// fired when the "<see cref="DetailsButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the "<see cref="DetailsButton"/>" button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void DetailsButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			if (this.progressLabel.IsVisible)
			{
				this.progressLabel.Visibility=Visibility.Hidden;
			}
			else
			{
				this.progressLabel.Visibility=Visibility.Visible;
			}
		}

		/// <summary>
		/// fired when the "<see cref="LegendButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the "<see cref="LegendButton"/>" button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void LegendButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			LegendWindow legendWindow=new LegendWindow ();
			legendWindow.Owner=this;

			legendWindow.ShowDialog ();
		}

		/// <summary>
		/// fired when the "<see cref="SettingsButton"/>" button is clicked
		/// </summary>
		/// <param name="sender">
		/// the "<see cref="SettingsButton"/>" button
		/// </param>
		/// <param name="routedEventArgs">
		/// some event-related data
		/// </param>
		private void SettingsButton_Click (object sender, RoutedEventArgs routedEventArgs)
		{
			SettingsWindow settingsWindow=new SettingsWindow ();
			settingsWindow.Owner=this;
			bool? dialogResult=settingsWindow.ShowDialog ();

			if (dialogResult==true)
			{
			}
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
			Random progressValuesGenerator=new Random ();

			await Task.Run (() =>
			{
				this.Timer.Start ();

				while (true)
				{
					if (this.IsHalted==false)
					{
						Application.Current.Dispatcher.Invoke (() => //An exception may pop up here when the "MainWindow" window is closing. It doesn't matter as we exit anyway
						{
							if (this.IsPaused==false)
							{
								if (this.ToggleUserGreeting==true)
								{
									this.GreetUser ();
									this.ToggleUserGreeting=false;
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
		/// stops the defragmentation
		/// </summary>
		private void StopDefragmentation ()
		{
			this.Timer.Stop ();

			this.IsHalted=true;
			this.StopButton.IsEnabled=false;
			this.PauseButton.IsEnabled=false;
			this.progressLabel.Content=this.progressLabel.Content.ToString ().Replace ("en cours", "arrêtée");
		}

		/// <summary>
		/// pauses the defragmentation
		/// </summary>
		private void PauseDefragmentation ()
		{
			this.Timer.Stop ();

			this.IsPaused=true;
			this.PauseButton.Content="Reprendre";
		}

		/// <summary>
		/// resumes the defragmentation
		/// </summary>
		private void ResumeDefragmentation ()
		{
			this.Timer.Start ();

			this.IsPaused=false;
			this.PauseButton.Content="Pause";
		}

		/// <summary>
		/// toggles a boolean allowing to greet the user
		/// </summary>
		/// <param name="source">
		/// the <see cref="Timers.Timer.Elapsed"> event
		/// </param>
		/// <param name="elapsedEventArgs">
		/// some event-related data
		/// </param>
		private void AllowToGreetUser (object source, Timers.ElapsedEventArgs elapsedEventArgs)
		{
			this.ToggleUserGreeting=true;
		}

		/// <summary>
		/// greets the user by showing a friendly visual message
		/// </summary>
		private void GreetUser ()
		{
			for (int i=0; i<this.topPane.Children.Count; i++)
			{
				((Rectangle) this.topPane.Children [i]).Fill=this.topPane.Background;
			}

			List <int> blockIndexes=this.GetUserGreetingBlockIndexes ();

			for (int i=0; i<blockIndexes.Count; i++) //filling the required blocks
			{
				((Rectangle) this.topPane.Children [blockIndexes [i]]).Fill=new SolidColorBrush (this.availableColors [1]); //in red
			}
		}

		/// <summary>
		/// determines which blocks to show when greeting the user
		/// </summary>
		/// <returns>
		/// the indexes list used by the "<see cref="GreetUser"/>" method
		/// </returns>
		private List <int> GetUserGreetingBlockIndexes ()
		{
			List <int> blockIndexes=new List <int> (0);
			int index=129;

			//The middle finger is the most important one. Did I ever say the message is friendly?
			for (int i=0; i<7; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=130;

			for (int i=0; i<7; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//index
			index=283;

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=284;

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//ring finger
			index=287; //183

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=288; //184

			for (int i=0; i<4; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//pinky
			index=341;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			index=342;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index+=52;
			}

			//thumb
			index=436;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index-=53;
			}

			index=437;

			for (int i=0; i<3; i++)
			{
				blockIndexes.Add (index);
				index-=53;
			}

			//rest of the hand
			for (int i=1; i<9; i++) //bottom
			{
				blockIndexes.Add (602-1*i); //602=550+52
			}

			//remaining space
			blockIndexes.AddRange (Enumerable.Range (489, 10));
			blockIndexes.AddRange (Enumerable.Range (541, 10));

			return (blockIndexes);
		}
	}
}