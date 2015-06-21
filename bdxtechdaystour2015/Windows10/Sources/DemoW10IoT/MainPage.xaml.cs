using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.Devices.Gpio;

namespace DemoW10IoT
{
	public sealed partial class MainPage : Page
	{
		private GpioPin _ledPin;
		private GpioPin _btnPin;
		private GpioPinValue _nextLedValue = GpioPinValue.High;

		public MainPage()
		{
			InitializeComponent();

			var gpio = GpioController.GetDefault();

			_btnPin = gpio.OpenPin(5);
			_ledPin = gpio.OpenPin(27);

			_ledPin.Write(GpioPinValue.Low);
			_ledPin.SetDriveMode(GpioPinDriveMode.Output);
			_btnPin.SetDriveMode(GpioPinDriveMode.Input);

			_btnPin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
			_btnPin.ValueChanged += buttonPin_ValueChanged;
		}

		private void buttonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
		{
			if (e.Edge == GpioPinEdge.FallingEdge)
			{
				_ledPin.Write(_nextLedValue);
				_nextLedValue = (_nextLedValue == GpioPinValue.Low) ? GpioPinValue.High : GpioPinValue.Low;

				var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
				{
					string visualState = (_nextLedValue == GpioPinValue.High) ? "OffVisualState" : "OnVisualState";

					VisualStateManager.GoToState(this, visualState, true);
				});
			}
		}
	}
}
