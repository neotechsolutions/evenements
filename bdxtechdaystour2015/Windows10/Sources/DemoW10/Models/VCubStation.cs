using System.Runtime.Serialization;

namespace DemoW10.Models
{
	/// <summary>
	/// VCub station data model
	/// </summary>
	public class VCubStation
	{
		/// <summary>
		/// Gets or sets the station identifier
		/// </summary>
		public int StationId { get; set; }

		/// <summary>
		/// Gets or sets the station title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the station address
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the station city
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the station latitude
		/// </summary>
		public double Latitude { get; set; }

		/// <summary>
		/// Gets or sets the station longitude
		/// </summary>
		public double Longitude { get; set; }

		/// <summary>
		/// Gets or sets the available slots
		/// </summary>
		public int AvailableSlots { get; set; }

		/// <summary>
		/// Gets or sets the available vehicules
		/// </summary>
		public int AvailableVehicules { get; set; }

		/// <summary>
		/// Gets or sets the total available slots
		/// </summary>
		public int TotalSlots { get; set; }

		/// <summary>
		/// Gets or sets the station type
		/// </summary>
		public string StationType { get; set; }

		/// <summary>
		/// Gets or sets the station correspondances
		/// </summary>
		public string Correspondance { get; set; }

		/// <summary>
		/// Gets or sets indication wether station has payment terminal
		/// </summary>
		public bool HasPaymentTerminal { get; set; }

		/// <summary>
		/// Gets or sets indication wether station is online
		/// </summary>
		public bool IsStationOnline { get; set; }
	}
}