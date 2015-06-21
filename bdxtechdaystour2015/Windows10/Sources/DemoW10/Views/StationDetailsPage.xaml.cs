using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DemoW10.Views
{
	/// <summary>
	/// Page displaying station details
	/// </summary>
	public sealed partial class StationDetailsPage : Page
	{
		/// <summary>
		/// Initialize a new instance
		/// </summary>
		public StationDetailsPage()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			DataContext = e.Parameter;

			base.OnNavigatedTo(e);
		}
	}
}