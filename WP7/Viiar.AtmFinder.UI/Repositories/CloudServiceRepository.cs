using System;
using System.Collections.ObjectModel;
using System.Device.Location;
using Microsoft.Phone.Reactive;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.UI.CloudServiceReference;

namespace Viiar.AtmFinder.UI.Repositories
{
    public class CloudServiceRepository : IEntityRepository
    {
        public void GetNearBy(GeoCoordinate location, Action<ServiceResponse, string> callback, Action<Exception> errorCallback)
        {
            var proxy = new AtmFinderServiceClient();

            var completedObservable = Observable.FromEvent<FindNearbyCompletedEventArgs>(
                ev => proxy.FindNearbyCompleted += ev,
                ev => proxy.FindNearbyCompleted -= ev);

            completedObservable.Subscribe(result =>
                                              {
                                                  if (result.EventArgs.Error != null)
                                                  {
                                                      if (errorCallback != null)
                                                      {
                                                          errorCallback.Invoke(result.EventArgs.Error);
                                                      }

                                                      return;
                                                  }

                                                  if (callback != null)
                                                  {
                                                      callback.Invoke(result.EventArgs.Result, "online");
                                                  }
                                              });

            proxy.FindNearbyAsync(location.Latitude,
                                  location.Longitude,
                                  new ObservableCollection<string>(AppSettings.Instance.MyChainNameSetting),
                                  AppSettings.Instance.ShowOnlyMyBankSetting,
                                  AppSettings.Instance.ShowClosestSetting);
        }
    }
}
