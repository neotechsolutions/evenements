using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DemoW10.Views
{
	public sealed partial class ShellView : UserControl
	{
		#region DPs
		/// <summary>
		/// Defines the Frame DependencyProperty used to perform application page navigation
		/// </summary>
		public static readonly DependencyProperty FrameProperty = DependencyProperty.Register("Frame", typeof(string), typeof(ShellView), null);

		#endregion

		#region Ctors
		/// <summary>
		/// Initialize a new instance
		/// </summary>
		public ShellView()
		{
			InitializeComponent();

			if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
			{
				HardwareButtons.BackPressed += OnHardwareBackButtonPressed;
				BackButton.Visibility = Visibility.Collapsed;
			}
		}
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets or sets the application frame
		/// </summary>
		public Frame Frame
		{
			get { return GetValue(FrameProperty) as Frame; }
			set
			{
				SetValue(FrameProperty, value);
			}
		}
		#endregion

		#region Handlers
		private bool BackRequested()
		{
			if (Frame == null)
				return false;

			if (Frame.CanGoBack)
			{
				Frame.GoBack();
				return true;
			}

			return false;
		}

		private void OnHardwareBackButtonPressed(object sender, BackPressedEventArgs e)
		{
			e.Handled = BackRequested();
		}
		
		private void OnBackButtonClick(object sender, RoutedEventArgs e)
		{
			BackRequested();
		}
		#endregion

	}
}
