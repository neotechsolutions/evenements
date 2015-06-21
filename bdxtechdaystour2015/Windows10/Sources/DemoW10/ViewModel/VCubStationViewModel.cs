using DemoW10.Models;
using GalaSoft.MvvmLight;
using Windows.Devices.Geolocation;

namespace DemoW10.ViewModel
{
	/// <summary>
	/// ViewModel used to manage a Vcub station
	/// </summary>
	public class VCubStationViewModel : ViewModelBase
	{
		#region Ctors
		/// <summary>
		/// Initialize a new instance
		/// </summary>
		/// <param name="stationData">The station data</param>
		public VCubStationViewModel(VCubStation stationData)
		{
			StationData = stationData;

			Position = new Geopoint(new BasicGeoposition()
			{
				Latitude = stationData.Latitude,
				Longitude = stationData.Longitude
			});

			//Compute availability percentage
			SlotAvailabilityPercentage = (int)((stationData.AvailableSlots / (double)stationData.TotalSlots) * 100);
			BikeAvailabilityPercentage = (int)(stationData.AvailableVehicules / (double)stationData.TotalSlots * 100);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the station data
		/// </summary>
		public VCubStation StationData
		{
			get; private set;
		}

		/// <summary>
		/// Gets or sets the station position
		/// </summary>
		public Geopoint Position
		{
			get; private set;
		}

		/// <summary>
		/// Gets the computed slot availability percentage
		/// </summary>
		public int SlotAvailabilityPercentage { get; private set; }
		
		/// <summary>
		/// Gets the computed bike availability percentage
		/// </summary>
		public int BikeAvailabilityPercentage { get; private set; }
		#endregion
	}
}
