using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Windows;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;
using Viiar.AtmFinder.UI.Location;

namespace Viiar.AtmFinder.UI.Repositories
{
    public class CashMachineRepository
    {
        private static readonly ObservableCollection<Entity> allMachines = new ObservableCollection<Entity>();
        private static readonly object syncRoot = new object();

        private static CashMachineRepository instance;
        private static readonly ObservableCollection<Entity> nearbyCashInMachines = new ObservableCollection<Entity>();
        private static readonly ObservableCollection<Entity> nearbyCashOutMachines = new ObservableCollection<Entity>();
        private static readonly ObservableCollection<Entity> nearbyMachines = new ObservableCollection<Entity>();
        private static readonly ObservableCollection<Entity> nearbyOffices = new ObservableCollection<Entity>();

        private static readonly List<string> localCachedIcons = new List<string>();

        public static CashMachineRepository Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new CashMachineRepository();
                    }
                }

                return instance;
            }
        }

        public event EventHandler<DataLoadEventArgs> DataLoadCompleted;

        public event EventHandler<DataLoadEventArgs> DataLoadFailed;

        public Entity GetById(Guid id)
        {
            return allMachines.First(e => e.Id == id);
        }

        public ObservableCollection<Entity> GetCurrentCashInList()
        {
            return nearbyCashInMachines;
        }

        public ObservableCollection<Entity> GetCurrentCashOutList()
        {
            return nearbyCashOutMachines;
        }

        public ObservableCollection<Entity> GetCurrentList()
        {
            return nearbyMachines;
        }

        public ObservableCollection<Entity> GetCurrentOfficeList()
        {
            return nearbyOffices;
        }

        public void SetCoordinates(GeoCoordinate location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            UnloadCurrentLists();

            // TODO: try to do both things at the same time and when online service fails, local one will be already ready

            if (AppSettings.Instance.IsOnlineServicesAvailable)
            {
                var cloudRepository = new CloudServiceRepository();
                cloudRepository.GetNearBy(location, OnEntityDataLoaded, OnEntityDataLoadFailed);
            }
            else
            {
                LoadOfflineRepository();
            }

            //CheckLoadedData();

            //var settings = new AppSettings();
            //if (!settings.IsLocationServicesAvailable)
            //{
            //    ClearCurrentLists();
            //    return;
            //}
        }

        public void UnloadCurrentLists()
        {
            lock (syncRoot)
            {
                //dataLoaded = false;
                ClearCurrentLists();
            }
        }

        private static void ClearCurrentLists()
        {
            allMachines.Clear();
            nearbyMachines.Clear();
            nearbyCashOutMachines.Clear();
            nearbyCashInMachines.Clear();
            nearbyOffices.Clear();
        }

        private void OnEntityDataLoaded(ServiceResponse response, string respositoryLocation)
        {
            if (response != null)
            {
                ClearCurrentLists();

                if (response.NearbyCashOutMachines != null)
                {
                    response.NearbyCashOutMachines.ForEach(LookupLocalIcons);
                    nearbyCashOutMachines.AddRange(response.NearbyCashOutMachines);
                    allMachines.AddRange(response.NearbyCashOutMachines);
                }

                if (response.NearbyCashInMachines != null)
                {
                    response.NearbyCashInMachines.ForEach(LookupLocalIcons);
                    nearbyCashInMachines.AddRange(response.NearbyCashInMachines);
                    allMachines.AddRange(response.NearbyCashInMachines);
                }

                if (response.NearbyOffices != null)
                {
                    response.NearbyOffices.ForEach(LookupLocalIcons);
                    nearbyOffices.AddRange(response.NearbyOffices);
                    allMachines.AddRange(response.NearbyOffices);
                }

                var tempList = allMachines.OrderBy(e => e.Distance).Distinct(new EntityIdComparer()).Take(AppSettings.Instance.ShowClosestSetting);
                nearbyMachines.AddRange(tempList);
            }

            OnDataCompleted(new DataLoadEventArgs(respositoryLocation));
        }

        private void LookupLocalIcons(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (IsIconCached(entity.Chain))
            {
                entity.SmallIcon = string.Format(CultureInfo.InvariantCulture, "icons/{0}-list.png", entity.Chain);
                entity.Icon = string.Format(CultureInfo.InvariantCulture, "icons/{0}-details.png", entity.Chain);
                entity.IconMap = string.Format(CultureInfo.InvariantCulture, "icons/{0}-map.png", entity.Chain);
            }
        }

        private bool IsIconCached(string chain)
        {
            if (localCachedIcons.Contains(chain))
            {
                return true;
            }

            var exists = Application.GetResourceStream(new Uri(string.Format(CultureInfo.InvariantCulture,
                                                                             "/Viiar.AtmFinder.UI;component/icons/{0}-list.png",
                                                                             chain),
                                                               UriKind.Relative)) != null;

            if (exists)
            {
                localCachedIcons.Add(chain);
            }

            return exists;
        }

        private void OnEntityDataLoadFailed(Exception error)
        {
            // TODO: move this to presentation layer
            if (!ApplicationState.OnlineRetrievalFailedMessageShown)
            {
                // TODO: add automatic fallback feature
                MessageBox.Show("Failed to load ATMs from online services..", AppResources.ErrorDialogTitle, MessageBoxButton.OK);
                ApplicationState.OnlineRetrievalFailedMessageShown = true;
            }

            LoadOfflineRepository();
        }

        private void LoadOfflineRepository()
        {
            var offlineRepository = new LocalRepository();
            offlineRepository.GetNearBy(DeviceLocationInfo.Current.CurrentCoordinates,
                                        OnEntityDataLoaded,
                                        exception =>
                                            {
                                                MessageBox.Show("Failed to load ATM from local storage..",
                                                                AppResources.ErrorDialogTitle,
                                                                MessageBoxButton.OK);

                                                OnDataLoadFailed(null);
                                            });
        }

        private void OnDataCompleted(DataLoadEventArgs e)
        {
            var handler = DataLoadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnDataLoadFailed(DataLoadEventArgs e)
        {
            var handler = DataLoadFailed;
            if (handler != null) handler(this, e);
        }
    }
}
