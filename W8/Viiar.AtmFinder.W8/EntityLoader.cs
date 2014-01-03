using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Viiar.AtmFinder.W8.Common;
using Viiar.AtmFinder.W8.DataSources;
using Viiar.AtmFinder.W8.OnlineServices;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace Viiar.AtmFinder.W8
{
    public class EntityLoader
    {
        private readonly Geolocator geolocator;

        static EntityLoader()
        {
            ApplicationSettings.Instance.PropertyChanged += OnApplicationSettingsChanged;
        }

        public EntityLoader()
        {
            this.geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.High };
        }

        public async Task<bool> LoadAsync()
        {
            var isCoordinatesAvailable = false;

            ApplicationState.Instance.IsDataLoading = true;
            try
            {
                var locations = await this.geolocator.GetGeopositionAsync().AsTask();
                ApplicationState.Latitude = locations.Coordinate.Latitude;
                ApplicationState.Longitude = locations.Coordinate.Longitude;

                isCoordinatesAvailable = true;

                ApplicationState.IsLocationPermissionDenied = false;
            }
            catch (UnauthorizedAccessException)
            {
                ApplicationState.IsLocationPermissionDenied = true;
            }
            catch (Exception)
            {
                // there could be situations when requesting coordinates could cause COMException - when network connection
                // is disabled and device does not have any other location service provider
                // TODO: log an exception
            }

            if (!isCoordinatesAvailable)
            {
                ApplicationState.Instance.IsDataLoading = false;
                var dialog = new MessageDialog("Failed to get your current coordinates. Check location service providers for more details.", "Oops!");
                await dialog.ShowAsync();
                return false;
            }

            try
            {
                if (ApplicationState.IsLocationPermissionDenied)
                {
                    if (!ApplicationState.IsLocationPermissionDeniedShow)
                    {
                        var dialog = new MessageDialog("Failed to load ATMs nearby. Permissions for Location Services are turned off.", "Oops!");
                        await dialog.ShowAsync();
                        ApplicationState.IsLocationPermissionDeniedShow = true;
                    }

                    return false;
                }
            }
            catch
            {
                // TODO: move message dialog to response handler instead of loader
                ApplicationState.Instance.IsDataLoading = false;
                return false;
            }

            if (ApplicationState.Latitude == 0 || ApplicationState.Longitude == 0)
            {
                ApplicationState.Instance.IsDataLoading = false;
                return false;
            }

            ApplicationState.Groups.ForEach(g => g.Items.Clear());
            var response = await LoadByCoordinatesAsync(ApplicationState.Latitude,
                                                        ApplicationState.Longitude,
                                                        Convert.ToInt32(ApplicationSettings.Instance.ItemsToShow));

            if (response == null)
            {
                var dialog =
                        new MessageDialog(
                                "Failed to load ATMs nearby from on-line services. It could be that services are down and we definitely are working on it.",
                                "Oops!");
                await dialog.ShowAsync();
                return false;
            }

            FillGroups(response);
            ApplicationState.Instance.IsDataLoading = false;
            ApplicationState.IsDataLoaded = true;
            return true;
        }

        public async Task<ServiceResponse> LoadByCoordinatesAsync(double latitude, double longitude, int count)
        {
            var service = AtmFinderServiceClient.CreateInstance();
            ServiceResponse response = null;

            try
            {
                response =
                        await
                        service.FindNearbyAsync(
                                                latitude,
                                                longitude,
                                                new ObservableCollection<string>(ApplicationSettings.Instance.MyBanks),
                                                ApplicationSettings.Instance.ShowOnlyMyBanks,
                                                count);
            }
            catch
            {
                // TODO: be more creative and find another way to handle errors
                ApplicationState.Instance.IsDataLoading = false;
            }

            return response;
        }

        private void FillGroups(ServiceResponse response)
        {
            if (response.NearbyCashOutMachines.Any())
            {
                var group1 = ApplicationState.Groups.First(g => g.UniqueId.Equals(EntityDataGroup.CashOut().UniqueId));
                response.NearbyCashOutMachines.ForEach(e => group1.Items.Add(e.ConvertToDataItem(group1)));
            }

            if (response.NearbyCashInMachines.Any())
            {
                var group2 = ApplicationState.Groups.First(g => g.UniqueId.Equals(EntityDataGroup.CashIn().UniqueId));
                response.NearbyCashInMachines.ForEach(e => group2.Items.Add(e.ConvertToDataItem(group2)));
            }

            if (response.NearbyOffices.Any())
            {
                var group3 = ApplicationState.Groups.First(g => g.UniqueId.Equals(EntityDataGroup.Office().UniqueId));
                response.NearbyOffices.ForEach(e => group3.Items.Add(e.ConvertToDataItem(group3)));
            }
        }

        private static void OnApplicationSettingsChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName.Equals("ItemsToShow"))
            {
                var loader = new EntityLoader();
                loader.LoadAsync();
            }
        }
    }
}
