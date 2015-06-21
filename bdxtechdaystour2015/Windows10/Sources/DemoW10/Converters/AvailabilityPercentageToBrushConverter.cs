using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace DemoW10.Converters
{
	/// <summary>
	/// Coverter used to convert a percentage to a brush value
	/// </summary>
	public class AvailabilityPercentageToBrushConverter : IValueConverter
	{
		/// <summary>
		/// The <see cref="Brush"/> to use when percentage > 20
		/// </summary>
		public Brush AvailableBrush { get; set; }

		/// <summary>
		/// The <see cref="Brush"/> to use when percentage == 0
		/// </summary>
		public Brush UnavailableBrush { get; set; }

		/// <summary>
		/// The <see cref="Brush"/> to use when percentage <= 20
		/// </summary>
		public Brush WarningBrush { get; set; }

		/// <summary>
		/// Converts a percentage value to a <see cref="Brush"/> value
		/// </summary>
		/// <param name="value">The percentage value</param>
		/// <param name="targetType">The target type</param>
		/// <param name="parameter">The parameter value (not used)</param>
		/// <param name="culture">The requested culture</param>
		/// <returns>A <see cref="Brush"/> value</returns>
		public object Convert(object value, Type targetType, object parameter, string culture)
		{
			int percentage = (int)value;

			if (percentage == 0)
				return UnavailableBrush;

			if (percentage <= 20)
				return WarningBrush;

			return AvailableBrush;
		}

		/// <summary>
		/// Not implemented
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, string culture)
		{ throw new NotImplementedException(); }
	}
}