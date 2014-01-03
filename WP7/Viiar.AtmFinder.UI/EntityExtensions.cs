using System;
using System.Device.Location;
using Viiar.AtmFinder.Contracts;

namespace Viiar.AtmFinder.UI
{
    public static class EntityExtensions
    {
        public static GeoCoordinate GetGeoCoordinates(this Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return new GeoCoordinate(entity.Latitude, entity.Longitude);
        }
    }
}
