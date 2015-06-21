using DemoW10.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DemoW10.Views
{
	/// <summary>
	/// Main application page used to display station list
	/// </summary>
	public sealed partial class MainPage : Page
	{
		/// <summary>
		/// Initialize a new instance
		/// </summary>
		public MainPage()
		{
			InitializeComponent();

			ViewModelLocator.Main.CurrentState = AdaptiveStates.CurrentState.Name;
		}

		private void OnAdaptiveStatesCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
		{
			((MainViewModel)DataContext).CurrentState = e.NewState.Name;
		}
	}
}
