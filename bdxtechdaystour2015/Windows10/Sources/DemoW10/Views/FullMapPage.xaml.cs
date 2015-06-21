using DemoW10.ViewModel;
using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace DemoW10.Views
{
	/// <summary>
	/// Page used to display some map features
	/// </summary>
	public sealed partial class FullMapPage : Page
	{
		#region Members
		private MapAnimationKind animationKind = MapAnimationKind.Linear;
		#endregion

		#region Ctors
		public FullMapPage()
		{
			InitializeComponent();

			if (myMap.Is3DSupported)
			{
				myMap.Style = MapStyle.Aerial3DWithRoads;
				animationKind = MapAnimationKind.Bow;
			}
			else
			{
				myMap.Style = MapStyle.AerialWithRoads;
			}

			ModeList.ItemsSource = new List<MapStyle>() { MapStyle.Aerial, MapStyle.Aerial3D, MapStyle.Aerial3DWithRoads, MapStyle.AerialWithRoads, MapStyle.Road, MapStyle.Terrain };
			ModeList.SelectedIndex = 0;
		}
		#endregion

		#region Methods
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
		private async void OnStationListSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				var station = StationList.SelectedItem as VCubStationViewModel;
				if (station != null)
				{
					if (myMap.CustomExperience != null)
					{
						ChangeStreetsideLocation(station.Position);
					}
					else
					{
						Boolean result = await myMap.TrySetViewAsync(station.Position, 18, 0, 0, animationKind);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void OnModeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			myMap.Style = (MapStyle)ModeList.SelectedValue;
		}

		private void OnTrafficCheckBoxChecked(object sender, RoutedEventArgs e)
		{
			myMap.TrafficFlowVisible = TrafficCheckBox.IsChecked.HasValue ? TrafficCheckBox.IsChecked.Value : false;
		}

		private void OnStreetSideClick(object sender, RoutedEventArgs e)
		{
			if (myMap.CustomExperience != null)
			{
				myMap.CustomExperience = null;
				myMap.ZoomLevel = 18;
				return;
			}

			var station = StationList.SelectedItem as VCubStationViewModel;
			Geopoint newLocation;
			if (station != null)
			{
				newLocation = station.Position;
			}
			else
			{
				newLocation = myMap.Center;
			}

			ChangeStreetsideLocation(newLocation);
		}

		private void OnAddElementClick(object sender, RoutedEventArgs e)
		{
			MainViewModel vm = DataContext as MainViewModel;
			myMap.MapElements.Clear();

			foreach (var station in vm.Stations)
			{
				myMap.MapElements.Add(new MapIcon()
				{
					Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/BikePin.png")),
					Location = station.Position,
					NormalizedAnchorPoint = new Point(0.5, 1.0),
					Title = station.StationData.Title
				});
			}
		}
		#endregion

	}
}
