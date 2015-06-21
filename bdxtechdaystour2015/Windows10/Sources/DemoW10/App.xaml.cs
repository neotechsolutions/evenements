using DemoW10.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DemoW10
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class App : Application
	{
		Frame rootFrame;
		public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;

		public App()
		{
			TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			ShellView shell = Window.Current.Content as ShellView;

			if (shell == null)
			{
				shell = new ShellView();

				if (rootFrame == null)
				{
					rootFrame = new Frame();
					rootFrame.NavigationFailed += OnNavigationFailed;
					
					Window.Current.Content = rootFrame;
				}

				shell.Frame = rootFrame;
				Window.Current.Content = shell;
			}

			if (rootFrame.Content == null)
				rootFrame.Navigate(typeof(MainPage), e.Arguments);

			Window.Current.Activate();
		}



		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
}
