using DemoW10.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;

namespace DemoW10.ViewModel
{
	/// <summary>
	/// Main application ViewModel
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		#region Members
		//Data
		private ObservableCollection<VCubStationViewModel> _stations;
		private VCubStationViewModel _selectedStation;
		private bool _isPaneOpened;
		private string _currentState;

		//Commands
		private RelayCommand<string> _showSectionCommand;
		private RelayCommand _toggleMenuCommand;

		//Services
		private INavigationService _navigationService;
		#endregion

		#region Ctors
		/// <summary>
		/// Initialize a new instance
		/// </summary>
		/// <param name="navigationService">The navigation service</param>
		public MainViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;

			LoadStations();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets all available stations
		/// </summary>
		public ObservableCollection<VCubStationViewModel> Stations
		{
			get { return _stations ?? (_stations = new ObservableCollection<VCubStationViewModel>()); }
		}

		/// <summary>
		/// Gets or sets the selected station
		/// </summary>
		public VCubStationViewModel SelectedStation
		{
			get { return _selectedStation; }

			set
			{
				//If small state : need navigation :)
				if ((CurrentState == "SmallState") && (_selectedStation != null))
				{
					_navigationService.NavigateTo("StationDetailsPage", value);
				}

				Set(ref _selectedStation, value);			
			}
		}

		/// <summary>
		/// Gets or sets indication wether pane is opened
		/// </summary>
		public bool IsPaneOpened
		{
			get { return _isPaneOpened; }

			set { Set(ref _isPaneOpened, value); }
		}

		/// <summary>
		/// Gets or sets the current state
		/// </summary>
		public string CurrentState
		{
			get { return _currentState; }

			set { Set(ref _currentState, value); }
		}
		#endregion

		#region Commands
		/// <summary>
		/// Gets the command used to display application sections
		/// </summary>
		public RelayCommand<string> ShowSectionCommand
		{
			get
			{
				return _showSectionCommand ?? (_showSectionCommand = new RelayCommand<string>((pageKey) =>
				{
					if (string.IsNullOrEmpty(pageKey))
						return;
					_navigationService.NavigateTo(pageKey);
				}));
			}
		}

		/// <summary>
		/// Gets the command used to toggle menu visibility
		/// </summary>
		public RelayCommand ToggleMenuCommand
		{
			get
			{
				return _toggleMenuCommand ?? (_toggleMenuCommand = new RelayCommand(() => IsPaneOpened = !IsPaneOpened));
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Loads all available station
		/// </summary>
		private async void LoadStations()
		{
			if (!IsInDesignMode)
			{
				String json = await PathIO.ReadTextAsync("ms-appx:///Assets/bikes.json");
				List<VCubStation> stations = JsonConvert.DeserializeObject<List<VCubStation>>(json);

				foreach (VCubStation s in stations)
				{
					Stations.Add(new VCubStationViewModel(s));
				}
			}
			else
			{
				//Sample data for design time
				Stations.Add(new VCubStationViewModel(new VCubStation { StationId = 1, Title = "Allees de Chartres", Address = "face au 1 allees de Chartres", City = "BORDEAUX", Latitude = 44.8472, Longitude = -0.57131, AvailableSlots = 18, AvailableVehicules = 2, TotalSlots = 20 }));
				Stations.Add(new VCubStationViewModel(new VCubStation { StationId = 2, Title = "Arts et Metiers", Address = "face au 1 allees de Chartres", City = "BORDEAUX", Latitude = 44.8472, Longitude = -0.57131, AvailableSlots = 16, AvailableVehicules = 10, TotalSlots = 26 }));
				Stations.Add(new VCubStationViewModel(new VCubStation { StationId = 3, Title = "Barbey", Address = "9, 10 place Dormoy", City = "TALENCE", Latitude = 44.8472, Longitude = -0.57131, AvailableSlots = 0, AvailableVehicules = 18, TotalSlots = 18 }));
			}

			SelectedStation = Stations[0];
		}

		#endregion
	}
}