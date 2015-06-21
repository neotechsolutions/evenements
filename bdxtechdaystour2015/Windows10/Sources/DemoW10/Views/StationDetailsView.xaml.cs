using DemoW10.ViewModel;
using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace DemoW10.Views
{
	/// <summary>
	/// View used to display station details
	/// </summary>
	public sealed partial class StationDetailsView : UserControl
	{
		#region Members
		private MapAnimationKind animationKind = MapAnimationKind.Linear;
		#endregion

		#region Ctors
		/// <summary>
		/// Initialize a new instance
		/// </summary>
		public StationDetailsView()
		{
			InitializeComponent();

			InitializeMap();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Initialize map data
		/// </summary>
		private void InitializeMap()
		{
			if (myMap.Is3DSupported)
			{
				myMap.Style = MapStyle.Aerial3DWithRoads;
				animationKind = MapAnimationKind.Bow;
			}
			else
			{
				myMap.Style = MapStyle.AerialWithRoads;
			}

			DataContextChanged += OnDataContextChanged;
		}

		private async void ChangeStreetsideLocation(Geopoint point)
		{
			if (myMap.IsStreetsideSupported)
			{
				StreetsidePanorama panoramaNearSpaceNeedle = await StreetsidePanorama.FindNearbyAsync(point);

				if (panoramaNearSpaceNeedle != null)
				{
					myMap.CustomExperience = new StreetsideExperience(panoramaNearSpaceNeedle);
				}
			}
		}
#endregion

		#region Handlers
		private async void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			try
			{
				var station = DataContext as VCubStationViewModel;
				if (station != null)
				{
					if (myMap.CustomExperience != null)
					{
						ChangeStreetsideLocation(station.Position);
					}
					else
					{
						Boolean result = await myMap.TrySetViewAsync(station.Position, 18, 0, 0, animationKind);

						myMap.MapElements.Clear();

						myMap.MapElements.Add(new MapIcon()
						{
							Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/BikePin.png")),
							Location = station.Position,
							NormalizedAnchorPoint = new Point(0.5, 1.0),
							Title = station.StationData.Title
						});
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void OnStreetSideClick(object sender, RoutedEventArgs e)
		{
			if (myMap.CustomExperience != null)
			{
				return;
			}

			ChangeStreetsideLocation(myMap.Center);
		}
		#endregion
		
	}
}
