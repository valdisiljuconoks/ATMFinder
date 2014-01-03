using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;
using Viiar.AtmFinder.Core.Repositories;
using Viiar.AtmFinder.Core.Repositories.Latvia;

namespace Viiar.AtmFinder.UI.Repositories
{
    public class LocalRepository : IEntityRepository
    {
        private static readonly ObservableCollection<Entity> allMachines = new ObservableCollection<Entity>();
        private static readonly object syncRoot = new object();
        private static bool dataLoaded;
        private readonly ConcurrentQueue<IEnumerable<Entity>> queue = new ConcurrentQueue<IEnumerable<Entity>>();

        public void GetNearBy(GeoCoordinate location, Action<ServiceResponse, string> callback, Action<Exception> errorCallback)
        {
            CheckLoadedData();
            callback.Invoke(FilterData(location), "offline");
        }

        private void CheckLoadedData()
        {
            if (dataLoaded)
            {
                return;
            }

            lock (syncRoot)
            {
                if (dataLoaded)
                {
                    return;
                }

                LoadData();
                dataLoaded = true;
            }
        }

        private void LoadData()
        {
            var tasks = new List<Task>();
            var repositories = CountryRepositoryBase.Current.Repositories;
            repositories.ForEach(
                r =>
                    {
                        var t = new Task(CollectEntitiesFromRepositories, r);
                        tasks.Add(t);
                        t.Start();
                    });

            Task.WaitAll(tasks.ToArray());
            if (this.queue.IsEmpty)
            {
                return;
            }

            allMachines.Clear();
            IEnumerable<Entity> current;
            while (this.queue.TryDequeue(out current))
            {
                ((List<Entity>)current).ForEach(i => allMachines.Add(i));
            }
        }

        private void CollectEntitiesFromRepositories(object state)
        {
            if (state == null)
            {
                return;
            }

            var repository = state as BaseRepository;
            if (repository == null)
            {
                return;
            }

            this.queue.Enqueue(repository.RetrieveEntities());
        }

        private ServiceResponse FilterData(GeoCoordinate location)
        {
            IEnumerable<Entity> tempList = allMachines.ToList();

            if (AppSettings.Instance.ShowOnlyMyBankSetting)
            {
                tempList = tempList.FilterOnlyMyBanks(AppSettings.Instance.MyChainNameSetting);
            }

            var nearbyCashOutMachines = new List<Entity>();
            if (AppSettings.Instance.ShowATMSetting)
            {
                nearbyCashOutMachines = FilterAndSort(
                    tempList,
                    location,
                    e => e.EntityType == EntityType.Atm
                         && (e.CashDirection & CashDirection.Out) == CashDirection.Out);
            }

            var nearbyCashInMachines = new List<Entity>();
            if (AppSettings.Instance.ShowATMCashInSetting)
            {
                nearbyCashInMachines = FilterAndSort(
                    tempList,
                    location,
                    e => e.EntityType == EntityType.Atm
                         && (e.CashDirection & CashDirection.In) == CashDirection.In);
            }

            var nearbyOffices = new List<Entity>();
            if (AppSettings.Instance.ShowOfficesSetting)
            {
                nearbyOffices = FilterAndSort(
                    tempList,
                    location,
                    e => e.EntityType == EntityType.Branch);
            }

            return new ServiceResponse
                       {
                           NearbyCashInMachines = nearbyCashInMachines,
                           NearbyCashOutMachines = nearbyCashOutMachines,
                           NearbyOffices = nearbyOffices
                       };
        }

        private List<Entity> FilterAndSort(IEnumerable<Entity> list,
                                           GeoCoordinate location,
                                           Func<Entity, bool> predicate)
        {

            // TODO: commissions calculation can be extracted to core
            // TODO: very close distance should be extracted to base repository
            var tempList = list.Where(predicate)
                .ForEach(i => i.CalculateDistance(location.Latitude, location.Longitude))
                .OrderBy(i => i.Distance).Take(AppSettings.Instance.ShowClosestSetting)
                .ForEach(e =>
                             {
                                 e.ShowCommissionWithdrawalSign =
                                     !FriendlyChainHelper.AreFirends(AppSettings.Instance.MyChainNameSetting, e.Chain);

                                 if (e.Distance < 0.02M)
                                 {
                                     e.DistanceView = "(" + AppResources.SomewhereHere + ")";
                                 }
                             });

            return tempList.Any() ? tempList.ToList() : new List<Entity>();
        }
    }
}
