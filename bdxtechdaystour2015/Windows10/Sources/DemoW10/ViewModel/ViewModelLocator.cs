using DemoW10.Services;
using DemoW10.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace DemoW10.ViewModel
{
	/// <summary>
	/// Application ViewModel locator
	/// </summary>
	public class ViewModelLocator
	{
		/// <summary>
		/// Initialize a new instance of the locator
		/// </summary>
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			var navigationService = CreateNavigationService();
			SimpleIoc.Default.Register(() => navigationService);

			SimpleIoc.Default.Register<MainViewModel>();
		}

		/// <summary>
		/// Gets the main ViewModel of the applciation
		/// </summary>
		public static MainViewModel Main
		{
			get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
		}

		private static INavigationService CreateNavigationService()
		{
			var navigationService = new FrameNavigationService();
			navigationService.Configure("FullMapPage", typeof(FullMapPage));
			navigationService.Configure("MainPage", typeof(MainPage));
			navigationService.Configure("StationDetailsPage", typeof(StationDetailsPage));
			navigationService.Configure("FeedbackPage", typeof(FeedbackPage));
			navigationService.Configure("SettingsPage", typeof(SettingsPage));

			return navigationService;
		}


		/// <summary>
		/// Cleanup all resources
		/// </summary>
		public static void Cleanup()
		{
		}
	}
}