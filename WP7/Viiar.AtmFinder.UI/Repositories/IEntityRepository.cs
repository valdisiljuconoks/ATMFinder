using System;
using System.Device.Location;
using Viiar.AtmFinder.Contracts;

namespace Viiar.AtmFinder.UI.Repositories
{
    public interface IEntityRepository
    {
        void GetNearBy(GeoCoordinate location, Action<ServiceResponse, string> callback, Action<Exception> errorCallback);
    }
}
